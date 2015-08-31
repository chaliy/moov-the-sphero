'use strict';

var React = require('react');
var ReactD3 = require('react-d3-components');
var LineChart = ReactD3.LineChart;

var d3 = require('d3');

var Reflux = require('reflux');

var store = require('../stores/eventsStore.js');
//var actions = require('../actions/eventsActions.js');

var Events = React.createClass({

  mixins: [Reflux.connect(store)],

  renderChart: function(label, key, member, domain){

    var data = [{
        label: label,
        values: (this.state.events || []).map(function(e, i){
          return { x: i, y: e[key][member] };
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

    if (!this.state.events){
      return <div>Loading...</div>;
    }


    return (
      <div>
        
        {this.renderChart('Acceleration X', 'acceleration', 'x', [3, -3])}
        {this.renderChart('Acceleration Y', 'acceleration', 'y', [3, -3])}
        {this.renderChart('Acceleration Z', 'acceleration', 'z', [3, -3])}

        {this.renderChart('Velocity X', 'velocity', 'x', [10, -10])}
        {this.renderChart('Velocity Y', 'velocity', 'y', [10, -10])}
        {this.renderChart('Velocity Z', 'velocity', 'z', [10, -10])}


        {this.renderChart('Gyroscope X', 'rawGyroscope', 'x', [500, -500])}
        {this.renderChart('Gyroscope Y', 'rawGyroscope', 'y', [500, -500])}
        {this.renderChart('Gyroscope Z', 'rawGyroscope', 'z', [500, -500])}
        {this.renderChart('Acceleration X', 'rawAccelerometer', 'x', [3, -3])}
        {this.renderChart('Acceleration Y', 'rawAccelerometer', 'y', [3, -3])}
        {this.renderChart('Acceleration Z', 'rawAccelerometer', 'z', [3, -3])}

      </div>
    );
  }
});

module.exports = Events;
