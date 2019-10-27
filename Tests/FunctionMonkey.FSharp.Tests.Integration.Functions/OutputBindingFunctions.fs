module FunctionMonkey.FSharp.Tests.Integration.Functions.OutputBindingFunctions
open FunctionMonkey.FSharp
open System
open FunctionMonkey.FSharp.Models
open FunctionMonkey.FSharp.Configuration
open FunctionMonkey.Tests.FSharp.Integration.Functions.ServiceBusFunctions

type HttpTriggerServiceBusQueueOutputCommand =
    {
        markerId: Guid
    }
    
type HttpTriggerServiceBusQueueCollectionOutputCommand =
    {
        markerId: Guid
    }
    
    
let private serviceBusQueueOutput (command:HttpTriggerServiceBusQueueOutputCommand) : ServiceBusQueuedMarkerIdCommand =
    {
        markerId = command.markerId
    }
    
let private serviceBusQueueCollectionOutput (command:HttpTriggerServiceBusQueueCollectionOutputCommand) : ServiceBusQueuedMarkerIdCommand list =
    [ { markerId = command.markerId } ]
    

let outputBindingFunctions = functions {
    httpRoute "outputBindings" [
        azureFunction.http (Handler(serviceBusQueueOutput), Get, subRoute = "/toServiceBusQueue")
            |> OutputBindings.serviceBusQueue Constants.ServiceBus.markerQueue
        azureFunction.http (Handler(serviceBusQueueCollectionOutput), Get, subRoute= "/collectionToServiceBusQueue")
            |> OutputBindings.serviceBusQueue Constants.ServiceBus.markerQueue
    ]
}

