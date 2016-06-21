
## Installation

1. Compile the project
2. Copy the .dll and .pdb file to `C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\130\Extensions`

### Rules

**SG.001 - Tables should be named with CamelCase**

Correct
- `TableName` 
- `TableName_Name`

Incorrect
- `tableName`
- `table_name`
- `TableName_name`
- `TABLE_NAME`

**SG.002 - Views should be prefixed with v then CamelCase**

Correct
- `vViewName`
- `vView`

Incorrect
- `vwViewName`
- `ViewName`
- `VIEW_NAME`

**SG.003 - Stored Procedures should be prefixed with uSp then CamelCase**

**SG.004 - Functions should be prefixed with uFn then CamelCase**

