# TagScanner4
TagScanner4 gathers ID3 tags and other available metadata from suitable files, e.g. MP3s, and stores them in a library file (binary and XML serialization formats are available). Loaded metadata are editable, and when the library file is re-saved, these edits can optionally be applied to the relevant media files.

There is a query builder allowing the construction of complex filters based on all metadata properties, and a Find/Replace which operates across multiple tags and optionally uses Regex.

The app is WinForms based, but uses embedded WPF grids to take advantage of their (free) filtering, sorting & grouping operations.
