open Fable.Python.Builtins
 
builtins.``open``(StringPath "test.txt", OpenTextMode.Write).write("Hello World, from Fable.Python! :)")

let openFile = builtins.``open``(StringPath "test.txt", OpenTextMode.Read)
builtins.print(openFile.read())
