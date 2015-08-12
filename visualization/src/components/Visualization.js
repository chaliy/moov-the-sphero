'use strict';

var React = require('react');
var ReactD3 = require('react-d3-components');
var LineChart = ReactD3.LineChart;

var d3 = require('d3');

var Reflux = require('reflux');

var store = require('../stores/eventsStore.js');
//var actions = require('../actions/eventsActions.js');

var Visualization = React.createClass({

  mixins: [Reflux.connect(store)],

  renderChart: function(label, key, domain){

    var data = [{
        label: label,
        values: this.state.events.map(function(e, i){
          return { x: i, y: e[key] };
        })
    }];

    var height = 150;

    return (
      <div>
        <h3>{label}</h3>
        <LineChart
          data={data}
          yScale={d3.scale.linear().domain(domain).range([0, height - 20])}
          width={800}
          height={height}
          margin={{top: 10, bottom: 20, left: 50, right: 10}}
          />
      </div>
    );
  },

  render: function() {

    // var data = [{
    //     label: 'Gyroscope X',
    //     values: this.state..map(function(e, i){
    //       return { x: i, y: e.gyroscopeX };
    //     })
    // }, {
    //     label: 'Gyroscope Y',
    //     values: this.state..map(function(e, i){
    //       return { x: i, y: e.gyroscopeY };
    //     })
    // }, {
    //     label: 'Gyroscope Z',
    //     values: this.state..map(function(e, i){
    //       return { x: i, y: e.gyroscopeZ };
    //     })
    // }];

    if (!this.state.events){
      return <div>Loading...</div>;
    }

    return (
      <div>
        {this.renderChart('Gyroscope X', 'gyroscopeX', [500, -500])}
        {this.renderChart('Gyroscope Y', 'gyroscopeY', [500, -500])}
        {this.renderChart('Gyroscope Z', 'gyroscopeZ', [500, -500])}
        {this.renderChart('Acceleration X', 'accelerationX', [3, -3])}
        {this.renderChart('Acceleration Y', 'accelerationY', [3, -3])}
        {this.renderChart('Acceleration Z', 'accelerationZ', [3, -3])}
      </div>
    );
  }
});

module.exports = Visualization;
