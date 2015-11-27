using System;
using System.ComponentModel;

namespace TagScanner.Models
{
	[Flags]
	public enum TrackStatus
	{
		[Description("Unknown: the item has no recognised TrackStatus value.")]
		Unknown = 0x00,
		[Description("Current: the item's library entry exactly matches its media file.")]
		Current = 0x01,
		[Description("New: the item's media file does not yet have a corresponding saved library entry.")]
		New = 0x02,
		[Description("Pending: the item's library entry contains more recent edits than its media file.")]
		Pending = 0x04,
		[Description("Updated: the item's media file contains more recent edits than its library entry.")]
		Updated = 0x08,
		[Description("Deleted: the item's media file no longer exists; its library entry is orphaned.")]
		Deleted = 0x10,
		[Description("Changed: the item's status is a combination of New, Pending, Updated and/or Deleted.")]
		Changed = New | Pending | Updated | Deleted
	}
}
