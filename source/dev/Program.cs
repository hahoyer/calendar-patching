using hw.Helper;
using Ical.Net;
using Ical.Net.Serialization;

var icsFile = SmbFile
    .Select("q:\\download\\lernwerk*.ics")
    .Select(n => n.ToSmbFile())
    .OrderByDescending(f => f.ModifiedDate)
    .First();

var calendar = Calendar.Load(icsFile.String);
var events = calendar.Events.Where(@event => @event.Description.Trim() != "").ToArray();
calendar.Events.Clear();
calendar.Events.AddRange(events);

var serializer = new CalendarSerializer(new SerializationContext());
icsFile.String = serializer.SerializeToString(calendar);
return;