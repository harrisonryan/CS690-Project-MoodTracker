# CS690-Project-MoodTracker
Author: Harrison Ryan

## Project Overview

This Mood Tracker application is a console-based .NET program that allows users to record and monitor their daily mood and related wellness factors such as sleep, activities, and an available option to add related notes in each particular entry. The purpose of this application is to help users reflect on their daily habits and emotional patterns over time.

This project represents Version 1.0.0 (Iteration 1), focusing on required (core) functionality that provides immediate value to the user.

⸻

## FINAL Iteration 3 Updates (Version 3.0.0)

Version 3.0.0 represents the final release of the Mood Tracker application. This version completes all functional requirements originally defined during the requirements phase and finalizes the project for submission.

## Final Functional Requirements Completed

All 10 functional requirements are fully implemented:

- FR-1: Create new daily mood entry
- FR-2: Record mood rating
- FR-3: Record sleep hours
- FR-4: Record daily activities
- FR-5: View list of saved entries
- FR-6: View entry details
- FR-7: View mood summary report
- FR-8: View sleep and mood insight comparison
- FR-9: Edit an existing mood entry
- FR-10: Delete an existing mood entry

## Final Application Features

The final version allows users to:

- Add and save daily mood entries
- Record sleep, mood, activities, and optional notes
- View all saved entries
- View detailed entry information
- Edit existing entries
- Delete entries
- View overall mood summary reports
- Compare mood trends based on sleep patterns

This makes the application fully functional, useful, and complete for end users.

## Final Documentation Updates

All project documentation has been fully updated for the final release:

### User Documentation

Includes step-by-step instructions for all 10 functional requirements so users can successfully use every feature of the application.

### Development Documentation

Includes modular code structure, testing information, and project design updates for maintainability and future improvements.

### Deployment Documentation

Includes instructions for downloading, building, running, and testing the application using:

```bash
dotnet restore
dotnet build
dotnet run
dotnet test CS690-Project-MoodTracker.slnx

⸻

## NEW Iteration 2 Updates (Version 2.0.0)

Version 2.0.0 improves the application by adding modular & cohesive code structure, automated testing, and additional functional requirements.

## NEW Features Added

The following new functionality was added in Iteration 2:

- Edit an existing mood entry
- Delete an existing mood entry
- View a brief mood summary report
- View sleep and mood insight comparison

## Additional Functional Requirements Implemented

The following functional requirements were added:

- FR-7: View mood summary report
- FR-8: View sleep and mood insight comparison
- FR-9: Edit an existing mood entry
- FR-10: Delete an existing mood entry

## Improved Program Structure

The project was redesigned into separate modules for readability, maintenance, and testing.

### Modules Included:

### Program.cs
Handles menu navigation and user interaction

### MoodTracker.cs
Handles core logic such as adding, viewing, editing, and deleting entries

### MoodStorage.cs
Handles saving and loading entries using JSON file storage

### MoodAnalysisService.cs
Handles mood summaries, reports, and sleep/mood insight calculations

### MoodEntry.cs
Defines the structure of each mood entry object

## Automated Testing

Automated tests were added using xUnit for major modules.

### Test Files

- MoodTrackerTests.cs
- MoodStorageTests.cs
- MoodAnalysisServiceTests.cs

## Running Tests

Run tests from the project root using:

```bash
dotnet test CS690-Project-MoodTracker.slnx

⸻

### Iteration 1 Log:

## Features (Iteration 1)

The application currently supports the following functionality:
	•	Add a new daily mood entry
	•	Record a mood rating (scale of 1–10)
	•	Record hours of sleep
	•	Record daily activities
	•	Add optional notes
	•	View all saved mood entries
	•	View detailed information for a selected entry

## Functional Requirements Implemented

The following high-priority functional requirements are implemented:
	•	FR-1: Allow users to create a new daily mood entry
	•	FR-2: Allow users to record a mood rating
	•	FR-3: Allow users to record hours of sleep
	•	FR-4: Allow users to view a list of saved mood entries
	•	FR-5: Allow users to view details of a selected mood entry

## Technology Stack

	•	Language: C#
	•	Framework: .NET 10
	•	Application Type: Console/CLI Application
	•	Storage: JSON formatting (local file, for future ML intentions & application)
 	•	Local IDE (VSCode/VS Studio, etc.) & GitHub Codespaces

⸻

## Program File Structure

Project Structure
	•	Program.cs
Handles user interaction, menu navigation, and input validation
	•	MoodEntry.cs
Defines the structure of a mood entry object
	•	MoodTracker.cs
Handles data management, including saving and loading entries
	•	mood_entries.json
Stores user data persistently in JSON format (generated when entry saved)

⸻

## Data Storage Approach

All user entries are stored in a local JSON file (mood_entries.json). JSON was selected because it provides a structured and scalable format for storing multiple entries while allowing easy serialization and deserialization within C#, as I had intentions to embed an OpenAI ML API to sythesize data for analytical purposes for end user data digestion.

This approach avoids the need for a database while still maintaining persistent data between application runs.

⸻

## How to Run the Application

### Prerequisites
	•	.NET 10 SDK installed in relevant IDE
    •	Basic CLI Familiarity & Knowledge

### Steps

Open a terminal in the project directory and run:

```bash
dotnet restore
dotnet build
dotnet run
```

How the Application is Useful (Iteration 1 Justification)

Even at Version 1, the application provides meaningful value to users. It allows users to:
	•	Track their daily mood over time
	•	Record important factors such as sleep and activities
	•	Reflect on past entries through stored history

This enables basic self-awareness and pattern recognition, making the application functional and useful even without advanced features such as analytics or editing.

⸻

Limitations (Future Improvements)

The current version does not yet support:
	•	User authentication
	•	Muti-User Support

These features are planned for future iterations.

⸻

Version 3.0.0 (at editing of this README)
