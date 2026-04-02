# How to Use This Template

This guide shows you how to download this template and create your own project from it.

## Option 1: Clone and Rename (Recommended)

### Step 1: Clone the Repository
```bash
git clone <repository-url> MyNewProject
cd MyNewProject
```

### Step 2: Remove Git History (Start Fresh)
```bash
# Windows PowerShell
Remove-Item -Recurse -Force .git
git init

# Linux/Mac
rm -rf .git
git init
```

### Step 3: Rename the Solution and Projects

**Using PowerShell (Windows):**
```powershell
# Navigate to project root
cd C:\path\to\MyNewProject

# Rename solution file
Rename-Item "CleanArchitecture.sln" "MyNewProject.sln"

# Update solution file content
(Get-Content "MyNewProject.sln") -replace 'CleanArchitecture', 'MyNewProject' | Set-Content "MyNewProject.sln"

# Update all .csproj files
Get-ChildItem -Recurse -Filter "*.csproj" | ForEach-Object {
    (Get-Content $_.FullName) -replace 'CleanArchitecture', 'MyNewProject' | Set-Content $_.FullName
}

# Update Program.cs
(Get-Content "API\Program.cs") -replace 'CleanArchitecture', 'MyNewProject' | Set-Content "API\Program.cs"

# Update appsettings.json with your database name
(Get-Content "API\appsettings.json") -replace 'YourDatabaseName', 'MyNewProjectDb' | Set-Content "API\appsettings.json"
```

**Using Bash (Linux/Mac):**
```bash
# Navigate to project root
cd /path/to/MyNewProject

# Rename solution file
mv CleanArchitecture.sln MyNewProject.sln

# Update solution file content
sed -i 's/CleanArchitecture/MyNewProject/g' MyNewProject.sln

# Update all .csproj files
find . -name "*.csproj" -type f -exec sed -i 's/CleanArchitecture/MyNewProject/g' {} \;

# Update Program.cs
sed -i 's/CleanArchitecture/MyNewProject/g' API/Program.cs

# Update appsettings.json with your database name
sed -i 's/YourDatabaseName/MyNewProjectDb/g' API/appsettings.json
```

### Step 4: Update Database Scripts
Edit `Database/00-CreateDatabase.sql`:
```sql
-- Replace 'YourDatabaseName' with your actual database name
CREATE DATABASE [MyNewProjectDb];
```

Update `Database/01-CreateTables.sql` and `Database/02-SeedData.sql`:
```sql
USE [MyNewProjectDb]
```

### Step 5: Restore and Build
```bash
dotnet restore
dotnet build
```

### Step 6: Run the Project
```bash
cd API
dotnet run
```

Access Swagger at: https://localhost:7000/swagger

---

## Option 2: Download as ZIP

### Step 1: Download
1. Click "Code" → "Download ZIP"
2. Extract to your desired location
3. Rename the folder to your project name

### Step 2: Follow Steps 2-6 from Option 1

---

## Option 3: Use as .NET Template (Advanced)

### Create a Custom Template

**Step 1: Prepare Template**
Create a `.template.config` folder in the root:
```bash
mkdir .template.config
```

Create `.template.config/template.json`:
```json
{
  "$schema": "http://json.schemastore.org/template",
  "author": "Your Name",
  "classifications": ["Web", "API", "Clean Architecture"],
  "identity": "CleanArchitecture.Template",
  "name": "Clean Architecture Web API",
  "shortName": "cleanarch",
  "tags": {
    "language": "C#",
    "type": "project"
  },
  "sourceName": "CleanArchitecture",
  "preferNameDirectory": true
}
```

**Step 2: Install Template Locally**
```bash
# From the template root directory
dotnet new install ./
```

**Step 3: Create New Project from Template**
```bash
# Create new project
dotnet new cleanarch -n MyNewProject -o C:\Projects\MyNewProject

# Navigate to project
cd C:\Projects\MyNewProject

# Restore and run
dotnet restore
dotnet build
cd API
dotnet run
```

**Step 4: Uninstall Template (if needed)**
```bash
dotnet new uninstall CleanArchitecture.Template
```

---

## Quick Checklist After Setup

- [ ] Solution renamed to your project name
- [ ] All `.csproj` files updated
- [ ] `appsettings.json` connection string updated
- [ ] Database scripts updated with your DB name
- [ ] Project builds successfully (`dotnet build`)
- [ ] Project runs successfully (`dotnet run`)
- [ ] Swagger accessible at https://localhost:7000/swagger
- [ ] Health endpoint returns "Healthy"

---

## Next Steps

1. **Read the README.md** - Follow the step-by-step tutorial to build your first feature
2. **Create your database** - Run the SQL scripts in order
3. **Build your first entity** - Follow the Product example in README
4. **Add more features** - Use the same patterns for all features

---

## Troubleshooting

### "Project not found" errors
- Make sure you updated all `.csproj` files with the new project name
- Run `dotnet restore` again

### Database connection errors
- Check your connection string in `API/appsettings.json`
- Ensure SQL Server is running
- Verify database name matches in scripts and connection string

### Port already in use
- Edit `API/Properties/launchSettings.json`
- Change the port numbers to available ports

---

**Happy Coding! 🚀**
