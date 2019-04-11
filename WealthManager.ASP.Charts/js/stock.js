
import Sunburst from './charts/sunburst';
import LineGraph from './charts/line_graph';
import * as yahooApiUtil from './util/yahoo_api_util';
import {fetchStockQuote} from './util/alphaUtilAPI';
import STOCK_DATA from './data';


Sunburst(STOCK_DATA);
makeLineGraph("GOOGL");

// Listen to Searchbar submit
document.getElementById('searchbar-form').addEventListener('submit', (e)=>{
  e.preventDefault();
  const searchParams = e.currentTarget.searchedStock.value;
  makeLineGraph(searchParams.toUpperCase());
});

// Parse Response Data and Create Graph
function makeLineGraph(symbol){
  fetchStockQuote(symbol).then(res => {
    let stockInfo = res["Monthly Time Series"];
    let parseArray = Object.keys(stockInfo).map(key => {

      return {
        "date": key,
        "open": stockInfo[key]["1. open"],
        "high": stockInfo[key]["2. high"],
        "low": stockInfo[key]["3. low"],
        "close": stockInfo[key]["4. close"]
      }
    });
    LineGraph({[symbol]: parseArray});
  });
}
