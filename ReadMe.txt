Made with Unity 5.2.1f1
written by Felix Engl
Contact: felix.engl@hotmail.com 

Requires EditorCoroutines 0.8 (See: https://github.com/FelixEngl/EditorCoroutines)

Instruction:
    1. Create a google spreadsheet
    2. Click on share -> "Anyone with the link can view"
    3. Get the downloadlink for a TSV (Tab-Separated Values) [Files -> Download As -> tsv]
            The link should look like this: https://docs.google.com/spreadsheets/d/1PgOAo3Vd-cWHIei2N8Ox68ZSaypWMuQJY_wADSICpY8/export?format=tsv&id=1PgOAo3Vd-cWHIei2N8Ox68ZSaypWMuQJY_wADSICpY8&gid=0
    4. Fill the spreadsheet like the sheed-instruction:
    
    sheed-instruction:
    Start at (A:1)
    
    Column Formatting:
    
    Column A:
        Ignore instructor: Write for every row ignore or use
            -ignore- |    ignore the row
            -use-    |    use the row for data processing
    Column B:
        Name: Write the describing name for the field
    Column C:
        Type: Write the type of the values (already supported: int and float)
            int     |   internal usage: int?
            float   |   internal usage: float?
    Column D up to ZZZZ:
        The different values of the row
            Special values:
                -nv-        |   no value (empty)
                -rowend-    |   end of a row
            Formatting:
                int         |   10000
                float       |   0.001   (with a point)
                
                
     Row formatting:
        Last row of the document:
            -end-
        
     
     Example Spreadsheet:
        https://docs.google.com/spreadsheets/d/1PgOAo3Vd-cWHIei2N8Ox68ZSaypWMuQJY_wADSICpY8/edit#gid=0
        
     You can also look at the pdf.


	 To-Do:
		- More supported value-types
