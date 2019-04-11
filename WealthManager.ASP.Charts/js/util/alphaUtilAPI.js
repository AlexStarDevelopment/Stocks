import $ from 'jquery';

const key = "B2GAJIV5VTL4C0CT";

export const fetchStockQuote = (symbol) => {
  const url = `https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=${symbol}&apikey=${key}`;

  return $.ajax({
    method: "GET",
    url: url
  });
};

// https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=GOOGL&apikey=B2GAJIV5VTL4C0CT
