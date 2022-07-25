namespace Connectivity.Messages;

/// <summary>
/// В обектах ClassTag (например ST_Burner), содержится тэг Msg типа DWORD, каждый бит которого определяет состояние соответствующего сообщения
/// для данной струтуры. В конструкторе ClassTag создается объект MsgForClassTag в конструктор которого передается тэг Msg и список сообщений
/// с номером бита в Msg, статусом и текстом сообщения. По событию Msg.PropertyChanged проверяются все биты Msg и при 1 добавляется, а при 0 
/// удаляется соответствующее сообщение в MsgTable.Msgs. Причем, 31 бит является флагом первопричины останова котла.
/// </summary>
public class MsgForClassTag
{
    public List<(int status, int bit, string txt)> EventsText { get; }
    public event EventHandler<List<string>>? MsgChanged;
    private readonly Msg[] eventRows;
    private readonly IWriteMsg? writeMsg;
    private readonly DWORD? msgTag;

    public MsgForClassTag(DWORD tag, List<(int, int, string)> eventsText, IWriteMsg? writeMsg)
    {
        msgTag = tag ?? throw new ArgumentException($"Параметр {nameof(tag)} конструктора {nameof(MsgForClassTag)} не может быть null");
        EventsText = eventsText;
        eventRows = new Msg[EventsText.Count];
        msgTag.ValueChanged += MsgTag_ValueChanged; ;
        this.writeMsg = writeMsg;
    }

    public MsgForClassTag(MsgInternal msgInternal)
    {
        EventsText = msgInternal.EventsText;
        eventRows = new Msg[EventsText.Count];
        msgInternal.ValueChanged += MsgTag_ValueChanged;
        this.writeMsg = msgInternal.WriteMsg;
    }

    private List<string> GetActiveMsgs()
    {
        List<string> msgs = new();
        uint tag = msgTag;
        foreach (var (status, bit, txt) in EventsText)
        {
            if ((tag >> bit & 1) == 1)
            {
                msgs.Add(txt);
            }
        }
        return msgs;
    }

    private void MsgTag_ValueChanged(object? sender, uint e)
    {
        uint tag = e;
        bool initTag = sender switch
        {
            DWORD tag2 => tag2.InitTag,
            MsgInternal _ => initTag = true,
            _ => throw new ArgumentException($"{nameof(MsgForClassTag)}.{nameof(MsgTag_ValueChanged)}: не соответствует тип {nameof(sender)} - {sender?.GetType()}")
        };
        List<string> msgs = new();
        void UnpackMsgTag(int bit, string text, int status, ref Msg row)
        {
            if ((tag >> bit & 1) == 1)
            {
                if (row == null)
                {
                    MsgStatus msgStatus = (MsgStatus)status;
                    if (msgStatus == MsgStatus.Авария && (tag >> 31 & 1) == 1)
                    {
                        msgStatus = MsgStatus.Первопричина;
                    }
                    row = new(DateTime.Now, text, msgStatus);
                    row.ConfirmChanged += (msg) => MsgTable.Msgs.Remove(msg); // Квитирование сообщения, сообщение удаляется из текущих
                    MsgTable.Msgs.Insert(0, row);
                    if (initTag) // В архив писать только после инициализации тэга, т.е. при запуске скады активные сообщения не писать
                    {
                        writeMsg?.WriteMsg(row.Text, row.Status.ToString(), row.DateTime);
                    }
                }

                if (MsgChanged != null)
                {
                    msgs.Add(row.Text);
                }
            }
            else
            {
                if (row != null)
                {
                    MsgTable.Msgs.Remove(row);
                    row = null;
                }
            }
        }

        int j = 0;
        foreach (var (status, bit, txt) in EventsText)
        {
            UnpackMsgTag(bit, txt, status, ref eventRows[j++]);
        }
        MsgChanged?.Invoke(this, msgs);
    }
}
