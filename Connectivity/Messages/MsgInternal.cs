namespace Connectivity.Messages;

/// <summary>
/// Класс для передачи внутренних сообщений (не привязанных к тегам) в MsgForClassTag
/// </summary>
public class MsgInternal
{
    private uint tag;
    public List<(int, int, string)> EventsText { get; init; }
    public IWriteMsg? WriteMsg { get; init; }
    public event EventHandler<uint>? ValueChanged;

    public MsgInternal(List<(int, int, string)> eventsText, IWriteMsg? writeMsg)
    {
        EventsText = eventsText;
        WriteMsg = writeMsg;
    }

    public void SetMsg(int numBit)
    {
        SetResBit(numBit, true);
    }

    public void ResetMsg(int numBit)
    {
        SetResBit(numBit, false);
    }

    private void SetResBit(int numBit, bool value)
    {
        if (numBit is < 0 or > 31) throw new ArgumentOutOfRangeException(nameof(numBit));
        uint m = 1;
        m <<= numBit;
        uint uVal;
        if (!value)
        {
            m = ~m;
            uVal = tag & m;
        }
        else
        {
            uVal = tag | m;
        }
        if (tag != uVal)
        {
            tag = uVal;
            ValueChanged?.Invoke(this, tag);
        }
    }
}
