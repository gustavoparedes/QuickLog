# QuickLog

![1](https://github.com/user-attachments/assets/2e0cef5a-84c7-4e21-a0e5-d9ef05678238)

# Download Compiled Version

You can download it [here](https://github.com/gustavoparedes/QuickLog/releases/download/v0.3/QuickLogv0.3.rar)

**Quick Log** is a portable forensic tool designed to analyze Windows event logs.  
It presents logs pre-organized according to the methodology outlined in [this guide](https://cybersecuritynews.com/windows-event-log-analysis/), allowing rapid identification of key forensic artifacts.

Developed for digital forensics training with open-source tools, it is used in courses taught at **Internet Solutions S.A.S.**, Bogotá, Colombia.

> **Requirements:**  
> - Windows 10 (64-bit)  
> - Screen resolution of 1920x1080


Logs are organized into workspaces.

# Workspace

A workspace is a "container" of logs that can contain one or more .evtx files from one or more machines running Windows. Before you can start viewing logs, you must create a new workspace or open a previously created one. By default, a newly created workspace does not contain log files; you must add logs after creating the workspace. Additional logs can always be added. A workspace can also be opened to continue reviewing logs and can be closed when necessary.

# Log Acquisition

During log acquisition, Windows logs are read, and the most relevant fields are stored in a SQLite database. Once the reading and storage process is complete, the original log files are no longer needed, as the database will be used. Each log entry is a record in the database within the logs table, and each record contains the following fields with descriptive names:

TimeCreated, UserID, EventID, Machine, Level, LogName, EventMessage, EventMessageXML, and ActivityID.

**TimeCreated:**

The time the event was created, stored in UTC, so it must be adjusted to the correct time zone by extracting it from the windows registry and using the evidence's time zone to establish the real time.

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

# 4. Labels and Comments:

Options to create, delete, and assign labels, as well as to create, update, and delete comments.

![Labels and comments](https://github.com/gustavoparedes/QuickLog/assets/61228478/d3ef32fc-600c-42f7-81a3-238cf8f2a3ab)

Before you can use labels, you must create them using the Label Manager.

![Label Manager1](https://github.com/gustavoparedes/QuickLog/assets/61228478/14312224-cf85-46ad-96b8-c46a94a199a6)

Now, simply click on the blank cell in the "Name" column.

![Label Manager2](https://github.com/gustavoparedes/QuickLog/assets/61228478/91fa5f2e-2f75-4e07-a10a-ad88d930a84b)

Select a color in the "Color" column.

![Label Manager3](https://github.com/gustavoparedes/QuickLog/assets/61228478/ad6d52b5-6646-433e-ae1a-2e9ed5f3ac5d)

And then click "Save".

![Label Manager4](https://github.com/gustavoparedes/QuickLog/assets/61228478/413ac50f-c6ef-42d3-98ea-47426081559e)

![Label Manager5](https://github.com/gustavoparedes/QuickLog/assets/61228478/45f6f4f7-f117-4afd-89ed-a5d5e16ab8f9)

Now you can close the Label Manager window and return to it whenever you need to create or delete labels.

To apply labels, you must select the log or logs to which you want to apply the label.

## Selecting Logs:

Just click on each row or log to select it. Use the Ctrl or Shift keys to select multiple logs at once, just like in Windows Explorer.

![Selecting Logs](https://github.com/user-attachments/assets/73180d75-88f5-4c2a-b7f7-15c673005143)

Select multiple logs in a row by holding Shift.

![SelectLog3](https://github.com/gustavoparedes/QuickLog/assets/61228478/0a7a3aef-77d3-49f4-a839-53ecf249c830)

Or by holding the Ctrl key, just like in Windows Explorer.

![SelectLog4](https://github.com/gustavoparedes/QuickLog/assets/61228478/9dec77e6-9a73-424c-b035-0100afbb4e96)

Now that you have selected the log or logs, simply click on **"Add Label"**.

![AddLabel1](https://github.com/gustavoparedes/QuickLog/assets/61228478/e89efebd-fe3f-4bf3-a5de-6b4cdcf83754)

You will see a window with the labels created in the Label Manager:

![AddLabel2](https://github.com/gustavoparedes/QuickLog/assets/61228478/180423b4-7446-4138-a2d9-3e59fd5c3285)

Simply select the label you want to apply using the same selection method as for the logs, and click **"Set Label"**.

![AddLabel3](https://github.com/gustavoparedes/QuickLog/assets/61228478/5713289c-490b-4a14-87bd-5ee8ed76c1f3)

Once the label is applied, it will look like this:

![AddLabel4](https://github.com/gustavoparedes/QuickLog/assets/61228478/5a2a1277-7a69-47e5-a043-dddb61c0306f)

## Adding Comments:

To add comments, select the log (only one) you want to add a comment to and click **"Add Comment"**.

![AddComment1](https://github.com/gustavoparedes/QuickLog/assets/61228478/544c6fc3-3b08-4d52-ba26-446e8d59121b)

Use the text box to enter the comment you need.

![AddComment2](https://github.com/gustavoparedes/QuickLog/assets/61228478/572b1821-6085-4732-8976-e668fddca600)

Make sure to click **"Save Comment"**.

![AddComment3](https://github.com/gustavoparedes/QuickLog/assets/61228478/8755c012-9ecd-4e73-9a06-b322c48bd8c1)

![AddComment4](https://github.com/gustavoparedes/QuickLog/assets/61228478/72516cb0-3fde-48a9-aa8b-e70dbaff7b28)



## 5. Save To:

Options to export the logs currently displayed in the log table to **PDF** or **CSV**.

![SaveTo](https://github.com/gustavoparedes/QuickLog/assets/61228478/48892f2d-f599-4d7f-b28f-b57f8b738148)

Keep in mind that a **comma-separated file (CSV)** may cause issues when importing it into tools like **LibreOffice** or **Excel**. This is because the fields **EventMessage** and **EventMessageXML** may contain commas, which can lead to incorrect field separation.

![Comma Separation Issue](https://github.com/user-attachments/assets/ec84f92a-c268-4055-ac5a-27056107c7ba)

For this reason, when exporting logs to **CSV**, the separator used is three consecutive characters:  
**`***`**

## 6. Time-Related Filters:

![TimeFilters](https://github.com/gustavoparedes/QuickLog/assets/61228478/a99c0939-c0e4-4b64-968d-9ec532fdb755)

Allows you to create a filter based on the timestamp of two logs, taking the **earliest timestamp** as the lower limit and the **latest timestamp** as the upper limit.  
For example, this can be used to view all logs generated during a user's session.

**First, select the two logs you want to use for the time range filter. Then, click the "Time Range" button.**

![TimeRange](https://github.com/gustavoparedes/QuickLog/assets/61228478/6b2ef126-1b13-4fca-9755-74619f9ae6c7)

You can also create a time filter for a **specific number of minutes** around an event's timestamp.  
For example, if an event occurred at **14:01:31** and we use the **"Minutes Around"** option with **1 minute**, it will filter all events between **one minute before and one minute after**, meaning from **14:00:31 to 14:02:31**.


## 7. Log Console:

Displays operation messages.

## 8. Custom Filters:

Allows granular filtering of any field in each log.  
Keep in mind that **basic filters** only display categorized events.  
Basic **custom filters** can be created, including text search options; this text will be searched in the **EventMessage** and **EventMessageXML** fields.

![Filter](https://github.com/user-attachments/assets/0d71436b-2063-4d37-9e60-6d30f82a0b64)

Filters can be applied to **all log fields**. The search logic works as follows:  
- Between **different fields**, the search applies an **AND** operation.  
- Within **the same field**, it applies an **OR** operation.

For example, to search for all logs with **EventID 5615**, regardless of any other condition, the query would be:

![FilterBar1](https://github.com/user-attachments/assets/831e6770-020c-4ad4-b256-eb5d12f5ff8a)

If we add another condition, for example, the user **S-1-5-18**, the search will combine both conditions using an **AND** operator.

![FilterBar2](https://github.com/user-attachments/assets/f4aa5224-ff27-48b6-8d89-63894bb3eeb4)

This means it will find all logs where the user is **S-1-5-18** **AND** the **EventID** matches.

Now, let's say we want to get all logs where the user is **S-1-5-18** and the **EventID is 5615 or 5617**.

![FilterBar3](https://github.com/user-attachments/assets/8b9eec53-8831-48b5-a355-3298b7ddc936)

Adding one more condition, for example, that the log contains the word **Management**.

![FilterBar4](https://github.com/user-attachments/assets/1cc34bcb-d50f-4407-a1d5-7816834dbcc3)

This way, you can customize the filter to make it more granular and specific.

## **Search Term:**

Searches within the **EventMessage** or **EventMessageXML** fields and allows the use of logical operators **AND** and **OR**.

For example, you can search for: `-1001`

![Search1](https://github.com/user-attachments/assets/b2d9018f-10ea-40e2-b92f-1936e72d8793)

Or search for: `-1001 AND logontype'>2<`

![Search2](https://github.com/user-attachments/assets/20760c99-9d54-4631-8a13-934aaa2316bf)

It will find matches whether **AND** or **OR** conditions are used within the **EventMessage** or **EventMessageXML** fields.

You can also use **regular expressions (RegExp)** for searching by enabling the **Regexp** option.

![Regexp](https://github.com/user-attachments/assets/7402fdac-4f09-4fb2-986c-6b907391612d)

In the example above, `-100[12].*?LogonType'>2<'`, we are searching for `-1001` or `-1002`, followed by:  
- Any character (`.`)  
- Any number of times (`*`)  
- That may or may not be present (`?`)  
- Then **LogonType'>2<'**

This allows us to find all interactive logins for users **1001** and **1002**.

## 9. Progress Bar:

The progress bar displays the loading progress of logs into the database as well as the log processing status.

![Processing2](https://github.com/gustavoparedes/QuickLog/assets/61228478/91ecb5a7-3a78-42ce-b0f3-be907d4bfb8a)

---

## **Workflow:**

The basic process involves loading one or multiple logs (usually all) from one or multiple machines, then searching for logs related to activities of interest, adding **labels** and **comments**, and finally creating a **timeline** of sessions or significant events, arranging them in **chronological order**.

![Timeline1](https://github.com/gustavoparedes/QuickLog/assets/61228478/d68134d1-a69a-4c2c-95ba-ceae46f6b200)

### **Step 1: Create a Workspace**

The first step is to create a **workspace**.

## **Create / Open / Close a Workspace:**

![Workspaces](https://github.com/gustavoparedes/QuickLog/assets/61228478/1f3b0da8-bea2-4ee2-9b18-7d947ec7f59c)

### **Step 2: Acquire Logs**

Next, add logs using the **"Acquire Logs"** option for individual or multiple files, or use **"Process Log Folder"** to process all `.evtx` files inside a folder.  
The logs will be stored in the database and classified based on predefined categories.

### **Basic Filters:**

![BasicFilters3](https://github.com/gustavoparedes/QuickLog/assets/61228478/ea292296-9407-4188-8f0d-e96d53af7b08)
![BasicFilters4](https://github.com/gustavoparedes/QuickLog/assets/61228478/affe8b14-62a3-480d-b86a-0bcea6698e0b)

### **Step 3: Review Classified Logs**

At the end of the process, all logs will be classified, and users found in the logs will be displayed.

![Final1](https://github.com/gustavoparedes/QuickLog/assets/61228478/aab04536-60c5-4f0f-a90e-fcceeb8bfd60)

---

### **Portable Execution:**
The compiled program can be executed from a **USB drive, external disk, or network folder** without requiring installation.
