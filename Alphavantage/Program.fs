open System.Text.Json.Serialization
open FsHttp

let getTickerInfoUrl ticker = $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={ticker}&apikey={token}"
let getEtfHoldingsUrl isin = $"https://dng-api.invesco.com/cache/v1/accounts/en_GB/shareclasses/{isin}/holdings/index?idType=isin"

GlobalConfig.Json.defaultJsonSerializerOptions <-
    let options = JsonFSharpOptions
                        .Default()
                        .ToJsonSerializerOptions()
    options

type Company = {
    Symbol: string
    AssetType: string
    Name: string
    Country: string
    Sector: string
    Industry: string
}

type EtfConstituent = {
    name: string
    isin: Skippable<string>
    cusip: Skippable<string>
    weight: double
}

type EtfHoldings = {
    effectiveDate: System.DateTime
    holdings: EtfConstituent list
}

let getCompanyInfo url =
    async {
        let! responseHttp = http { GET url } |> Request.sendAsync
        return! responseHttp |> Response.deserializeJsonAsync<Company>
    }

let getEtfHoldings url =
    async {
        let! responseHttp = http { GET url } |> Request.sendAsync
        let! etfHoldings = responseHttp |> Response.deserializeJsonAsync<EtfHoldings>
        return etfHoldings.holdings
    }

let getEtfCompanyInfos url =
    async {
        let! etfHoldings = getEtfHoldings url
        let companyInfoTasks = etfHoldings |> Seq.map (fun etfConstituent -> getCompanyInfo (getTickerInfoUrl etfConstituent.name))
        return! companyInfoTasks |> Async.Parallel
    }

let companyInfos = getEtfCompanyInfos (getEtfHoldingsUrl "IE0032077012") |> Async.RunSynchronously
printf "%s" (string companyInfos)