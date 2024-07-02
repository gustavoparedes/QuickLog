


# QuickLog
Windows log viewer organized according to this job https://cybersecuritynews.com/windows-event-log-analysis/

Quick Log is a simple tool for viewing Windows logs in EVTX format. The logs are organized into workspaces. It requires Windows 10 64-bit and a resolution of 1920x1080.
If you are looking for the compiled version, you can download it from the releases link in the right column of this page.

# Workspace

A workspace is a "container" for logs that can hold one or more .evtx files from one or multiple machines running Windows. Before you can start viewing the logs, you need 
to create a new workspace or open a previously created one. By default, a newly created workspace does not contain any log files; you must add logs after creating or opening 
the workspace. Additional logs can always be added. A workspace can also be opened to continue reviewing logs and can be closed when necessary.

# Log Acquisition

During log acquisition, Windows logs are read and the most relevant fields are stored in a SQLite database. Once the reading and storage process is complete, the original 
log files are no longer needed, as the database will be used instead. Each log entry is a record in the database within the logs table, and each record contains the 
following fields with descriptive names:

TimeCreated, UserID, EventID, Machine, Level, LogName, EventMessage, EventMessageXML, and ActivityID.

TimeCreated:

The time at which the event was created, stored in UTC. When processing the logs, the time will be adjusted to the local machine's time zone. Keep this in mind and ensure 
you adjust to the correct time zone by extracting it from the record. Use the evidence's time zone to establish the actual time. For convenience, you could, for example, 
change the machine's time zone to match the evidence during the log processing.

UserID: 

The security descriptor of the user whose context is used to publish the event. For detailed information on this topic, see here:
https://learn.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2012-r2-and-2012/dn743661(v=ws.11)

EventID: 

he identifier of the event.

Machine: 

The name of the machine where this event was logged.

Level:

The level of the event. The level indicates the severity of the event.

LogName: 

The name of the event log where this event is recorded.

EventMessage: 

The event message in the current locale.

EventMessageXML: 

XML representation of the event. All event properties are represented in the event's XML.

ActivityID: 

A globally unique identifier (GUID) for the ongoing activity with which the event is associated.


# The Interface:


![Interfaz](https://github.com/gustavoparedes/QuickLog/assets/61228478/c2bec1e0-254a-44b4-9fe3-163e60e94938)

# 1. Acquire and Basic Filters:

   
The first three items are for:

- Previewing
- Acquiring one or more log files
- Acquiring all .evtx files under a folder or path, allowing you to add multiple logs from various machines organized in subfolders within a parent folder, for example.

  ![Basic Filters](https://github.com/gustavoparedes/QuickLog/assets/61228478/3cb35628-7da3-4189-98fd-27b4a405e85d)

From the fourth element onward, events are categorized into areas of interest based on the work 
shown here https://cybersecuritynews.com/windows-event-log-analysis/  with author credits to Forward Defence.

![Basic Filters1](https://github.com/gustavoparedes/QuickLog/assets/61228478/92e4ae0f-6f00-449d-86af-9017ac03bfb9)
![Basic Filters2](https://github.com/gustavoparedes/QuickLog/assets/61228478/6a929ce0-ad10-4ab7-a1b2-6d6dfee3c1af)





# 2. Log Table:

   Displays the logs based on the category selected in the Basic Filters.

![Tabla1](https://github.com/gustavoparedes/QuickLog/assets/61228478/a947b4bd-3c81-40ed-a756-6c757dc6609c)

You can navigate from cell to cell, and the content of each cell will be displayed in the text box as you move.

EventMessage Visualization

![Tabla2](https://github.com/gustavoparedes/QuickLog/assets/61228478/1f31e4f9-57f0-4b17-9d04-e6314e40a9b7)

EventMessageXML Visualization

![Tabla3](https://github.com/gustavoparedes/QuickLog/assets/61228478/d7a79259-4931-4b17-8dc9-5376a5c565ba)


# 3. Text Box:

   Displays the content of the selected cell using keyboard arrows or the mouse.
   It allows you to see highlighted search results and read the log contents comfortably.

   
![Tabla4](https://github.com/gustavoparedes/QuickLog/assets/61228478/873c2d83-3b91-4a2e-8d88-00061a5e8c5d)



# 4. Labels and Comments:

   Options to create, delete, and assign labels, as well as to create, update, and delete comments.

![Labels and comments](https://github.com/gustavoparedes/QuickLog/assets/61228478/d3ef32fc-600c-42f7-81a3-238cf8f2a3ab)


# 5. Save to:

   Options to export the logs currently displayed in the log table to PDF or CSV.

![SaveTo](https://github.com/gustavoparedes/QuickLog/assets/61228478/48892f2d-f599-4d7f-b28f-b57f8b738148)


# 6. Time-Related Filters:

![TimeFilters](https://github.com/gustavoparedes/QuickLog/assets/61228478/a99c0939-c0e4-4b64-968d-9ec532fdb755)


   Allows you to generate a filter based on the start time of one log and the end time of another,
   such as a user's session start and end times.
   

![Time Range](https://github.com/gustavoparedes/QuickLog/assets/61228478/6b2ef126-1b13-4fca-9755-74619f9ae6c7)


   You can also create a time filter for a specific number of minutes around
   the time of an event. For example, if an event occurred at 14:01:31 and you use the "Minutes around" option with one minute,
   it will filter all events between one minute before and one minute after, i.e., between 14:00:31 and 14:02:31.



# 7. Log Console:

   Displays operation messages


# 8. Custom Filters:

   Allows granular filtering by any of the fields in each log. Remember that basic filters only display categorized events.
   Basic custom filters can be created that include text search options; this text will be searched in the EventMessage and EventMessageXML fields.

![CustomFilter](https://github.com/gustavoparedes/QuickLog/assets/61228478/2fc087d0-5a5b-4c53-834c-0ed10be8b9ce)

   Filters can be applied to all fields of the logs. The search logic between different fields is an AND operation, meaning that the filter is applied as follows:

First, it must be within the time range as the primary condition, AND it must match the UserID, AND EventID, AND Machine Name, AND Level, AND LogName, AND Label, 
AND the search terms within either the EventMessage or EventMessageXML fields.

Search Term: Will search within the EventMessage or EventMessageXML fields and can use the logical operators AND and OR.

For example, you can search for: -1001

![Search1](https://github.com/gustavoparedes/QuickLog/assets/61228478/4e918d94-a504-4cd5-b3f2-8c3c9b9c8618)

Or search for: -1001 AND logontype'>2<

![Search2](https://github.com/gustavoparedes/QuickLog/assets/61228478/30698eb3-4a62-4620-8aa5-1b8a7ddcbeb9)


It will find search matches whether they are AND or OR conditions within either the EventMessage or EventMessageXML fields.

# 9. Progress bar:

   The progress bar displays the progress of logs being loaded into the database as well as the processing of the logs.








# The workflow: 

Basically to process one or several (usually all) logs from one or several machines and then start searching 
for logs related to activities of interest, put tags and comments and finally make a timeline for example with the relevant 
sessions or events that were logged sorted chronologically as a timeline.


![Time line1](https://github.com/gustavoparedes/QuickLog/assets/61228478/d68134d1-a69a-4c2c-95ba-ceae46f6b200)



The first thing to do is to create a workspace



# Create / Open / Close a Workspace:

![Work Spaces](https://github.com/gustavoparedes/QuickLog/assets/61228478/1f3b0da8-bea2-4ee2-9b18-7d947ec7f59c)



Afterward, add logs using the "Acquire Logs" option for one or multiple files or "Process Log Folder" to process all .evtx files within a folder.
The logs will be stored in the database and classified according to the predefined categories.



Basic Filters:


![Basic Filters3](https://github.com/gustavoparedes/QuickLog/assets/61228478/ea292296-9407-4188-8f0d-e96d53af7b08)
![Basic Filters4](https://github.com/gustavoparedes/QuickLog/assets/61228478/affe8b14-62a3-480d-b86a-0bcea6698e0b)

At the end of the processing, you will see all the logs classified, and the users found in the logs will be displayed.

![Finish1](https://github.com/gustavoparedes/QuickLog/assets/61228478/aab04536-60c5-4f0f-a90e-fcceeb8bfd60)



