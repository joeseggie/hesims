namespace HESIMS.Web.Converters;

public class CountryViewModelConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }

        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            var json = JsonDocument.Parse(stringValue);
            return new CountryViewModel
            {
                CountryId = json.RootElement.GetProperty("CountryId").GetGuid(),
                CountryName = json.RootElement.GetProperty("CountryName").GetString(),
                CountryCode = json.RootElement.GetProperty("CountryCode").GetString(),
            };
        }
        return base.ConvertFrom(context, culture, value);
    }
}