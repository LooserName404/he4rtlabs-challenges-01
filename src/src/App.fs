module App

open System
open Browser.Dom

let parse str =
    match Double.TryParse (str) with
    | true, n -> n
    | _ -> nan

let inputSelector id = document.querySelector($"#{id}") :?> Browser.Types.HTMLInputElement

let tempoDiarioInput = inputSelector "tempo-diario"
let diasEfetivosInput = inputSelector "dias-efetivos"
let diasFeriasInput = inputSelector "dias-ferias"
let totalProjetoInput = inputSelector "total-projeto"

let calcButton = document.querySelector(".calc-button") :?> Browser.Types.HTMLButtonElement

let visor = document.querySelector(".visor")

// Register our listener
calcButton.onclick <- fun _ ->
    let tempoDiarioValue = tempoDiarioInput.value
    let diasEfetivosValue = diasEfetivosInput.value
    let diasFeriasValue = diasFeriasInput.value
    let totalProjetoValue = totalProjetoInput.value

    let algumVazio =
        [tempoDiarioValue; diasEfetivosValue; diasFeriasValue; totalProjetoValue]
        |> List.exists String.IsNullOrWhiteSpace

    if algumVazio then
        visor.innerHTML <- "$ ___,__ / h"
        ()
    else
        let tempoDiario = parse tempoDiarioValue
        let diasEfetivos = parse diasEfetivosValue
        let diasFerias = parse diasFeriasValue
        let totalProjeto = parse totalProjetoValue

        let valorHora = totalProjeto / ((diasEfetivos + diasFerias) * tempoDiario)

        visor.innerHTML <- $"$ %.2f{valorHora} / h"

