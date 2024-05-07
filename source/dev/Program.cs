using hw.DebugFormatter;
using hw.Helper;
using Ical.Net;
using Ical.Net.Serialization;

var icsFile = SmbFile
    .Select("q:\\download\\lernwerk*.ics")
    .Select(n => n.ToSmbFile())
    .OrderByDescending(f => f.ModifiedDate)
    .First();

Strip(icsFile);
return;

void Strip(SmbFile smbFile)
{
    ("Converting "+ smbFile.FullName + " ...").Log();
    var calendar = Calendar.Load(smbFile.String);
    var events = calendar.Events.Where(@event => @event.Description.Trim() != "").ToArray();
    calendar.Events.Clear();
    calendar.Events.AddRange(events);

    var serializer = new ComponentSerializer(new SerializationContext());
    smbFile.String = serializer.SerializeToString(calendar);
    ("Converting "+ smbFile.FullName + " complete").Log();
}