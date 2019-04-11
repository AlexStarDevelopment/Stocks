import $ from 'jquery';
const STOCK_SYMS = ["AAPL", "GOOGL", "SQ", "TSLA", "VMW"];

// TODO: grab infomation from csv file

export const fetchStockQuotes = (stocks = STOCK_SYMS) => {
  const url = `https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22${stocks.join('%20')}%22)&env=store://datatables.org/alltableswithkeys&format=json`;

  return $.ajax({
    method: 'GET',
    url: url
  });
};

// https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes&env=store://datatables.org/alltableswithkeys&format=json
