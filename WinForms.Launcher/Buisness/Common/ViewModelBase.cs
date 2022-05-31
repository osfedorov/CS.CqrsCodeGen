using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WinForms.Launcher.Buisness.Common;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void FirePropertyChangedEvent([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    protected void SetWhenNotEqual(object value, [CallerMemberName] string pptName = "")
    {
        var type = GetType();
        var backingField = type.GetField(
                name: $"_{pptName}",
                bindingAttr: BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
        
        while (null != type.BaseType && null == backingField)
        {
            // For some reason privae filed of th ebase class is not returned by GetFiled()
            type = type.BaseType;
            backingField = type.GetField(
                name: $"_{pptName}",
                bindingAttr: BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
        }

        if (backingField is null)
        {
            throw new ArgumentException($"No backing field found for v \'{pptName}\' in {GetType()}", nameof(pptName));
        }

        if (Equals(backingField.GetValue(this), value))
        {
            return;
        }

        backingField.SetValue(this, value);
        FirePropertyChangedEvent(pptName);
    }
}