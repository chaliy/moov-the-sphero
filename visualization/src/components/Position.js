'use strict';

var React = require('react');
var vis = require('vis/dist/vis');

var Reflux = require('reflux');

var store = require('../stores/eventsStore.js');
//var actions = require('../actions/eventsActions.js');

var Position = React.createClass({

  mixins: [Reflux.connect(store)],

  componentDidMount: function(){


    // var data = new vis.DataSet();
    //
    // // // create some shortcuts to math functions
    // // var sqrt = Math.sqrt;
    // // var pow = Math.pow;
    // // var random = Math.random;
    // //
    // // // create the animation data
    // // var imax = 100;
    // // for (var i = 0; i < imax; i++) {
    // //   var x = pow(random(), 2);
    // //   var y = pow(random(), 2);
    // //   var z = pow(random(), 2);
    // //
    // //   var dist = sqrt(pow(x, 2) + pow(y, 2) + pow(z, 2));
    // //   var range = sqrt(2) - dist;
    // //
    // //   data.add({x: x, y: y, z: z, style: range});
    // // }
    //
    // (this.state.positions || []).forEach(function(p){
    //   data.add({x: p.position.x, y: p.position.y, z: p.position.z});
    // });

    // specify options
    var options = {
      width: '600px',
      height: '600px',
      style: 'dot-size',
      showPerspective: false,
      showGrid: true,
      keepAspectRatio: true,
      legendLabel: 'value',
      verticalRatio: 1.0,
      cameraPosition: {
        horizontal: -0.54,
        vertical: 0.5,
        distance: 1.6
      }
    };

    // create our graph
    var container = this.refs.container.getDOMNode();
    this.graph = new vis.Graph3d(container, [], options);
  },

  render: function() {

    // if (!this.state.events){
    //   return <div>Loading...</div>;
    // }

    if (this.graph){
      var data = new vis.DataSet();

      (this.state.positions || []).forEach(function(p){
        data.add({x: p.position.x, y: p.position.y, z: p.position.z});
      });

      this.graph.setData(data);
      this.graph.redraw();
    }

    return (
      <div>
        <div ref='container' />
      </div>
    );
  }
});

module.exports = Position;
