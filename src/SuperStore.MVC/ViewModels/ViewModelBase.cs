namespace SuperStore.MVC.ViewModels;
public abstract class ViewModelBase
{
    private Dictionary<string, List<string>> _errors = [];

    public void SetErrors(IDictionary<string, string[]> errors)
    {
        _errors = errors.ToDictionary(e => e.Key, e => e.Value.ToList());
    }

    public void SetErrors(IReadOnlyDictionary<string, List<string>>? errors)
    {
        if (errors == null)
            return;

        _errors = errors.ToDictionary(e => e.Key, e => e.Value);
    }

    public void AddError(string fieldName, string error)
    {
        if (!_errors.ContainsKey(fieldName))
            _errors[fieldName] = [];

        _errors[fieldName].Add(error);
    }

    public bool HasErrors(string fieldName)
        => _errors != null && _errors.ContainsKey(fieldName);

    public IEnumerable<string> GetErrors(string fieldName)
    {
        if (!HasErrors(fieldName))
            return [];

        return _errors[fieldName];
    }
}
