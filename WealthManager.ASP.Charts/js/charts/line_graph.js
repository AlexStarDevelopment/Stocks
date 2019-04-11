import * as d3 from 'd3';

const LineGraph = (data) => {
  // x:
    // date
  // y:
    // open
    // high
    // close
    // low

  // Add padding
  d3.select('#line-graph')
    .style('padding', '3em');

  let selector = d3.select('#line-graph svg');
  if(selector._groups[0] !== null) {
    selector.remove();
  }

  const symbol = Object.keys(data)[0];

  // set size of graph
  const width = 650,
        height = 350;

  // time format
  const parseTime = d3.timeParse("%Y-%d-%m");

  // set range and domain
  const x = d3.scaleTime().range([0, width]);
  const y = d3.scaleLinear().range([height, 0]);


  // Define lines
  const openLine = d3.line()
    .x(function(d) { return x(d.date); })
    .y(function(d) { return y(d.open); });

  const highLine = d3.line()
    .x(function(d) { return x(d.date); })
    .y(function(d) { return y(d.high); });

  const closeLine = d3.line()
    .x(function(d) { return x(d.date); })
    .y(function(d) { return y(d.close); });

  const lowLine = d3.line()
    .x(function(d) { return x(d.date); })
    .y(function(d) { return y(d.low); });

  // Append SVG to #line-graph div
  const svg = d3.select('#line-graph').append('svg')
      .attr('width', width)
      .attr('height', height)
      .style('padding', '2em')
    .append('g');



  function draw(data, symbol) {
    data = data[symbol];

    //format data
    data.forEach(function(d){
      // console.log(d.date);
      d.date = parseTime(d.date);
      d.open = +d.open;
      d.high = +d.high;
      d.low  = +d.low;
      d.close= +d.close;
    });

    //sort by date
    data.sort(function(a,b){
      return a.date-b.date;
    });
    // scale the range
    x.domain(d3.extent(data, function(d) { return d.date; }));
    y.domain([0, d3.max(data, function(d) {
  	  return Math.max(d.open, d.high, d.low, d.close);
    })]);


    // Add the lines path.
    svg.append("path")
        .data([data])
        .attr("class", "line")
        .style('stroke', 'red')
        .attr("d", openLine);



    svg.append("path")
        .data([data])
        .attr("class", "line")
        .attr("d", highLine);

    // svg.append("path")
    //     .data([data])
    //     .attr("class", "line")
    //     .attr("d", lowLine);
    // svg.append("path")
    //     .data([data])
    //     .attr("class", "line")
    //     .attr("d", closeLine);

    // add x axis
    svg.append('g')
        .attr('class', 'x axis')
        .attr("transform", "translate(0," + height + ")")
        .call(d3.axisBottom(x).tickFormat(d3.timeFormat("%d-%m-%Y")))
        .selectAll("text")
        .style("text-anchor", "end")
        .attr("dx", "-.8em")
        .attr("dy", ".15em")
        .attr("transform", "rotate(-20)");

    // add y axis
    svg.append("g")
      .attr('class', 'y axis')
      .call(d3.axisLeft(y));

    // Add Symbol Label
    d3.select('#company')
        .text(symbol);

    d3.select('.legend')
      .attr('visibility', '');
  }

  draw(data, symbol);


  //remove loader
  d3.select('.loader').remove();
  return 0;
};

export default LineGraph;
