namespace HESIMS.Web.Controllers;

public class BaseController : Controller
{
    protected Guid GetValueFromNullableGuid(Guid? nullableGuid)
    {
        var guidValue = nullableGuid ?? Guid.Empty;
        if (guidValue == Guid.Empty)
        {
            throw new ArgumentException("Guid value is empty");
        }

        return guidValue;
    }

    protected DateTimeOffset GetValueFromNullableDateTimeOffset(DateTimeOffset? nullableDateTimeOffset)
    {
        var dateTimeOffsetValue = nullableDateTimeOffset ?? DateTimeOffset.MinValue;
        if (dateTimeOffsetValue == DateTimeOffset.MinValue)
        {
            throw new ArgumentException("DateTimeOffset value is empty");
        }

        return dateTimeOffsetValue;
    }
}