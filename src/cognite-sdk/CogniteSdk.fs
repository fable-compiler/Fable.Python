module Fable.Python.CogniteSdk

open Fable.Core

type ITimeSeries =
    abstract plot : start: string * ``end``: string * aggregates: string array * granularity: string -> unit

type ITimeSeriesApi =
    abstract retrieve : id: int64 -> ITimeSeries
    abstract list : unit -> ITimeSeries list

[<Import("CogniteClient", from = "cognite.client")>]
type CogniteClient (?apiKey: string, ?api_subversion: string, ?project: string, ?clientName: string) =
    abstract member time_series : ITimeSeriesApi
    override this.time_series = nativeOnly
