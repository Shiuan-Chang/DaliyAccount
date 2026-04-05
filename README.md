#  DailyAccount

> A personal finance desktop application built with Windows Forms, supporting expense tracking, chart analysis, note management, and receipt image attachments.

---

##  Overview

DailyAccount is a lightweight daily expense tracker with an intuitive interface that lets users quickly log income and expenses, and analyze spending habits through multiple chart types. All data is stored locally in CSV format — no database installation required.

---

##  Features

| Page | Description |
|------|-------------|
| **AccountForm** | Query transaction records by date range with multi-column browsing |
| **AddForm** | Add expense / income entries with up to 2 receipt image attachments |
| **AnalysisForm** | Visualize spending distribution via Pie Chart, Line Chart, and Stacked Bar Chart |
| **NoteForm** | Browse, edit, and delete existing transaction records |
| **SettingForm** | Personal preference settings |

### Chart Analysis
- Group data by **Category**, **Purpose**, or **Payment Method**
- Filter by specific account types before analysis
- Supported chart types:  Pie Chart / Line Chart /  Stacked Bar Chart

---

##  Architecture

This project follows the **MVP (Model-View-Presenter)** pattern combined with the **Strategy Pattern** for flexible chart grouping logic.

```
DailyAccount/
├── Contract/           # Interface definitions for Views and Presenters
├── Forms/              # UI pages (View layer)
├── Models/             # Data models (DAO / DTO / Model)
├── Presenters/         # Business logic layer
├── Repository/         # Data access layer (CSV read/write)
├── Mappings/           # AutoMapper profiles
└── Utility/
    ├── Builder/        # ChartBuilder (Builder Pattern)
    ├── Strategy/       # Chart grouping and rendering strategies
    │   ├── GroupStrategy/    # Data grouping logic
    │   ├── SeriesStrategy/   # Chart series generation
    │   └── ChartStrategy/    # Chart area configuration
    └── ImageCompressionUtility.cs

DailyAccount.Tests/
├── DailyAccountTests.cs      # Unit tests
└── CSVIntegrationTests.cs    # Integration tests
```

### Design Patterns
- **MVP** — Complete separation of View and logic, enabling unit testing
- **Strategy Pattern** — Grouping, series generation, and chart rendering are independently extensible
- **Builder Pattern** — `ChartBuilder` chains the chart creation pipeline
- **Repository Pattern** — All data access is abstracted through the `IRepository` interface

---

##  Tech Stack

| Item | Details |
|------|---------|
| Language | C# / .NET Framework 4.7.2 |
| UI Framework | Windows Forms |
| Data Storage | CSV files (custom CSVHelper library) |
| Object Mapping | AutoMapper 10.0.0 |
| Chart Component | System.Windows.Forms.DataVisualization.Charting |
| Image Processing | System.Drawing (JPEG compression) |

---

##  Requirements

- Windows 10 or later
- .NET Framework 4.7.2
- Visual Studio 2022 (recommended)

---

##  Getting Started

1. Clone the repository
```bash
git clone https://github.com/your-username/DailyAccount.git
```

2. Open `DailyAccount.sln` in Visual Studio

3. Update the data storage path in `Repository/CSVRepository.cs`
```csharp
// Change this to your preferred storage location
public CSVRepository(string basePath = @"C:\Your\Path\Here")
```

4. Build and run the project (`F5`)

---

##  QA & Testing

### Test Project Structure

```
DailyAccount.Tests/
├── DailyAccountTests.cs      # Unit tests (33 test cases)
└── CSVIntegrationTests.cs    # Integration tests (12 test cases)
```

### Testing Tools

| Tool | Purpose |
|------|---------|
| xUnit 2.5.3 | Unit testing framework |
| Moq | Mock objects to isolate external dependencies |
| AutoMapper | Validate mapping configurations |

---

### Unit Tests — `DailyAccountTests.cs`

####  Core Business Logic (Strategy Layer)

| Test Class | Cases | Coverage |
|------------|-------|----------|
| `PieChartGroupingStrategyTests` | 7 | No-filter returns all, conditionType filtering, amount aggregation, invalid date skipping, non-numeric amount treated as 0, empty input handling |
| `LineChartGroupingStrategyTests` | 3 | Analysis mode grouping, non-analysis mode preserves raw data, decimal precision validation |
| `StackedChartGroupingStrategyTests` | 3 | Condition-only mode, analyze-only mode, combined condition + analyze |

####  Model Behavior

| Test Class | Cases | Coverage |
|------------|-------|----------|
| `AddModelTests` | 2 | `Reset()` clears all fields, constructor sets Date to current time |
| `AccountingModelTests` | 2 | Parameterized constructor sets all properties, default constructor does not throw |

####  Repository Boundary Validation

| Test Class | Cases | Coverage |
|------------|-------|----------|
| `CSVRepositoryDateValidationTests` | 5 | Invalid date formats throw `FormatException` in `AddData` and `RemoveData` |
| `CSVRepositoryDateRangeTests` | 2 | `end < start` returns empty list, same-day range queries only once |

####  MVP Presenter Flow

| Test Class | Cases | Coverage |
|------------|-------|----------|
| `AddPresenterTests` | 2 | Save success calls `View.ShowMessage`, failure does not |
| `NotePresenterTests` | 2 | Delete success calls `View.Reload()`, failure does not |
| `AnalysisPresenterTests` | 1 | `LoadData` completion triggers `View.UpdateDataView` |

####  Edge Cases

| Test Class | Cases | Coverage |
|------------|-------|----------|
| `AmountEdgeCaseTests` | 3 | Zero amount, very large values (near long max), negative refund aggregation |
| `CrossMonthGroupingTests` | 1 | December and January data grouped separately without mixing |

---

### Integration Tests — `CSVIntegrationTests.cs`

These tests verify the full data pipeline through `CSVRepository` using a temporary folder, ensuring real data is never touched during testing.

```
AddData → GetDatasByDate → Validate data consistency
AddData → RemoveData → Confirm deletion
Date range queries → Verify boundary behavior
```

| Test Case | Coverage |
|-----------|----------|
| `AddData_ThenGetByDate_ReturnsCorrectRecord` | Write and read back a single record |
| `AddData_MultipleRecords_AllRetrievable` | Multiple records across different days |
| `AddData_CreatesCSVFileInDateFolder` | Verifies correct folder/file structure |
| `RemoveData_ExistingDate_RecordsAreDeleted` | Delete target date records |
| `RemoveData_NonExistentDate_ReturnsFalse` | Returns false when nothing to delete |
| `RemoveData_OnlyDeletesTargetDate_OtherDatesUntouched` | Other dates remain intact |
| `GetDatasByDate_RecordsOutsideRange_AreExcluded` | Range filtering correctness |
| `GetDatasByDate_SameDayRange_ReturnsSingleDayRecords` | Single day query |
| `GetDatasByDate_EndBeforeStart_ReturnsEmpty` | Invalid range handling |
| `AnalysisGetDatasByDate_ReturnsRecordsInRange` | Analysis query pipeline |
| `AddData_InvalidDate_ThrowsFormatException` | Input validation |
| `RemoveData_InvalidDate_ThrowsFormatException` | Input validation |

---

### Running Tests

```bash
# In Visual Studio
# Test → Test Explorer → Run All Tests

# Or via command line
dotnet test
```

---

### Key Design Decision — Dependency Injection in CSVRepository

`CSVRepository` was refactored to accept an injectable `basePath` parameter, enabling integration tests to use a temporary folder without touching real data:

```csharp
// Production usage — uses default path
var repo = new CSVRepository();

// Test usage — uses temp folder
var repo = new CSVRepository(_testFolder);
```

This design follows the **Dependency Injection** principle, decoupling the repository from a hardcoded file path and making the data layer fully testable in isolation.

---

##  Known Issues & Improvements

###  High Priority
- **Presenter coupling** — `AnalysisPresenter` and `NotePresenter` instantiate `CSVRepository` directly in their constructors. Recommend refactoring to constructor injection with `IRepository`.

###  Medium Priority
- **`amount` field type** — `AccountingModel.amount` is stored as `string`. Recommend changing to `decimal` with conversion at the DAO/DTO layer.
- **`NotePresenter.UpdateData` directly manipulates files** — Should be encapsulated behind the `IRepository` interface.

---

##  Data Format

Transaction data is stored in CSV with the following structure:

```
Date, AccountName, AccountType, Detail, PaymentMethod, Amount, OriginalPic1, OriginalPic2, CompressedPic1, CompressedPic2
2024-01-05 09:30, Food, Expense, Breakfast, Cash, 150, /path/pic1.jpg, , /path/compress1.jpg,
```

---

