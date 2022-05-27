using CqrsCodeGen.CodeGeneration.Action;
using CqrsCodeGen.CodeGeneration.ActionHandler;
using CqrsCodeGen.CodeGeneration.ActionRepsponseModel;
using CqrsCodeGen.CodeGeneration.Dto;
using CqrsCodeGen.CodeGeneration.GrpcService;
using CqrsCodeGen.Intrernals.Configuration;

namespace CqrsCodeGen.CodeGeneration;

public interface ICodeGenerator
{
    Task<bool> GenerateAsync();
}

internal sealed class CodeGenerator : ICodeGenerator
{
    private readonly ICodeGenConfiguration _configuration;

    public CodeGenerator(ICodeGenConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> GenerateAsync()
    {
        await Task.Run(DoWork);
        return true;
    }

    private void DoWork()
    {
        var action = new ActionTemplate { Config = _configuration.Action };
        var actionResponseModel = new ActionResponseModelTemplate { Config = _configuration.ActionResponse };
        var actionHandler = new ActionHandlerTemplate { Config = _configuration.ActionHandler };

        var request = new DtoTemplate { Config = _configuration.RequestDto };
        var response = new DtoTemplate { Config = _configuration.ResponseDto };
        var service = new GrpcServiceImplTemplate { Config = _configuration.GrpcService };

        File.WriteAllText(GetPath(_configuration.Action), action.TransformText());
        File.WriteAllText(GetPath(_configuration.ActionHandler), actionHandler.TransformText());

        if (!string.IsNullOrEmpty(_configuration.GrpcServiceName))
        {
            File.WriteAllText(GetPath(_configuration.RequestDto), request.TransformText());
            File.WriteAllText(GetPath(_configuration.ResponseDto), response.TransformText());
            File.WriteAllText(GetPath(_configuration.GrpcService), service.TransformText());
        }

        if (!_configuration.ActionResponse.IsExistingType)
        {
            File.WriteAllText(GetPath(_configuration.ActionResponse), actionResponseModel.TransformText());
        }
    }

    private string GetPath(CodeGenConfigBase cfgBase)
    {
        var nspace = cfgBase.Namespace.Split('.');
        var baseNspace = cfgBase.BaseNamespace.Split('.');

        var relPath = Path.Combine(nspace.Except(baseNspace).ToArray());
        var fullPath = Path.Combine(_configuration.OutputPath, cfgBase.BaseNamespace, relPath);

        Directory.CreateDirectory(fullPath);

        return Path.Combine(fullPath, cfgBase.ClassName + ".cs");
    }
}
