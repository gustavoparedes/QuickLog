# QuickLog

![1](https://github.com/user-attachments/assets/2e0cef5a-84c7-4e21-a0e5-d9ef05678238)

# Download Compiled Version

You can download it [here](https://github.com/gustavoparedes/QuickLog/releases/download/v0.3/QuickLogv0.3.rar)

**Quick Log** is a simple tool to visualize Windows logs in EVTX format, organized according to this work:
https://cybersecuritynews.com/windows-event-log-analysis/ and designed for digital forensics courses using open-source tools, taught at Internet Solutions S.A.S, Bogotá, Colombia. It requires Windows 10 64-bit and a resolution of 1920x1080.

Logs are organized into workspaces.

# Workspace

A workspace is a "container" of logs that can contain one or more .evtx files from one or more machines running Windows. Before you can start viewing logs, you must create a new workspace or open a previously created one. By default, a newly created workspace does not contain log files; you must add logs after creating the workspace. Additional logs can always be added. A workspace can also be opened to continue reviewing logs and can be closed when necessary.

# Log Acquisition

During log acquisition, Windows logs are read, and the most relevant fields are stored in a SQLite database. Once the reading and storage process is complete, the original log files are no longer needed, as the database will be used. Each log entry is a record in the database within the logs table, and each record contains the following fields with descriptive names:

TimeCreated, UserID, EventID, Machine, Level, LogName, EventMessage, EventMessageXML, and ActivityID.

**TimeCreated:**

The time the event was created, stored in UTC, so it must be adjusted to the correct time zone by extracting it from the log and using the evidence's time zone to establish the real time.

![UTC](https://github.com/user-attachments/assets/b9e54019-1823-4a36-9c11-52c2e9e84b50)

**UserID:**

The security descriptor of the user whose context is used to publish the event. For detailed information on this topic, refer to:
[https://learn.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2012-r2-and-2012/dn743661(v=ws.11)](https://learn.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2012-r2-and-2012/dn743661(v=ws.11))

**EventID:**

The event identifier.

**Machine:**

The name of the machine where this event was logged.

**Level:**

The event level. The level indicates the severity of the event.

**LogName:**

The name of the event log where this event is recorded.

**EventMessage:**

The event message in the current locale.

**EventMessageXML:**

XML representation of the event. All event properties are represented in the event XML.

**ActivityID:**

A globally unique identifier (GUID) for the ongoing activity with which the event is associated.

# Interface:

![Interface](https://github.com/gustavoparedes/QuickLog/assets/61228478/c2bec1e0-254a-44b4-9fe3-163e60e94938)

# 1. Acquisition and Basic Filters:

The first three elements are for:

- Previewing
- Acquiring one or more log files
- Acquiring all .evtx files within a folder or path, allowing multiple logs from various machines to be added, organized in subfolders within a main folder, for example.

![Basic Filters](https://github.com/gustavoparedes/QuickLog/assets/61228478/3cb35628-7da3-4189-98fd-27b4a405e85d)

From the fourth element onward, events are categorized into areas of interest based on the work shown [here](https://cybersecuritynews.com/windows-event-log-analysis/) with author credits to [Forward Defence](https://forwarddefense.com/).

![Basic Filters1](https://github.com/gustavoparedes/QuickLog/assets/61228478/92e4ae0f-6f00-449d-86af-9017ac03bfb9)
![Basic Filters2](https://github.com/gustavoparedes/QuickLog/assets/61228478/6a929ce0-ad10-4ab7-a1b2-6d6dfee3c1af)

# 2. Log Table:

Displays logs according to the category selected in Basic Filters.
Clicking on any row will display the full information in the text box on the right.

![Log Table](https://github.com/user-attachments/assets/4b7abb4a-6307-4dba-8e17-9659b94b655c)

# 3. Text Box:

Displays the content of the selected row, allows search results to be highlighted, and enables comfortable reading of log content.

![Text Box](https://github.com/user-attachments/assets/2e0caeec-8cf9-4f03-b09e-c02585e821f6)

# 4. Tags and Comments:

Options to create, delete, and assign tags, as well as to create, update, and delete comments.

![Labels and comments](https://github.com/gustavoparedes/QuickLog/assets/61228478/d3ef32fc-600c-42f7-81a3-238cf8f2a3ab)

# 5. Save to:

Options to export the logs currently displayed in the log table to PDF or CSV.

![Save](https://github.com/gustavoparedes/QuickLog/assets/61228478/48892f2d-f599-4d7f-b28f-b57f8b738148)

# 6. Time Filters:

Allows filtering based on two timestamps, taking the earliest as the lower bound and the latest as the upper bound.

![Time Filters](https://github.com/gustavoparedes/QuickLog/assets/61228478/a99c0939-c0e4-4b64-968d-9ec532fdb755)

# 7. Log Console:

Displays operational messages.

# 8. Custom Filters:

Allows granular filtering based on any log field.

![Custom Filters](https://github.com/user-attachments/assets/0d71436b-2063-4d37-9e60-6d30f82a0b64)

# 9. Progress Bar:

Shows the progress of logs being loaded into the database and processed.

![Processing](https://github.com/gustavoparedes/QuickLog/assets/61228478/91ecb5a7-3a78-42ce-b0f3-be907d4bfb8a)

# Workflow:

The general process consists of acquiring and categorizing logs, applying tags and comments, and creating a timeline of relevant events.

![Timeline](https://github.com/gustavoparedes/QuickLog/assets/61228478/d68134d1-a69a-4c2c-95ba-ceae46f6b200)


The first step is to create a workspace.

# Create / Open / Close a Workspace:

![Workspace](https://github.com/gustavoparedes/QuickLog/assets/61228478/1f3b0da8-bea2-4ee2-9b18-7d947ec7f59c)

Then, add logs using the "Acquire Logs" option for one or multiple files or "Process Log Folder" to process all .evtx files within a folder. The logs will be stored in the database and categorized according to predefined categories.

Basic Filters:

![BasicFilters3](https://github.com/gustavoparedes/QuickLog/assets/61228478/ea292296-9407-4188-8f0d-e96d53af7b08)
![BasicFilters4](https://github.com/gustavoparedes/QuickLog/assets/61228478/affe8b14-62a3-480d-b86a-0bcea6698e0b)

At the end of the process, all logs will be classified, and the users found in the logs will be displayed.

![Final1](https://github.com/gustavoparedes/QuickLog/assets/61228478/aab04536-60c5-4f0f-a90e-fcceeb8bfd60)

The compiled program can be run from a USB drive, external disk, or network folder without installation.
