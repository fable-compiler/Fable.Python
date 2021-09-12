#if FABLE_COMPILER
module Program

()
#else
module Program =
    [<EntryPoint>]
    let main _ = 0
#endif
