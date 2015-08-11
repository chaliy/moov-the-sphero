'use strict';

var Reflux = require('reflux');
var actions = require('../actions/eventsActions.js');

var ReconnectingWebSocket = require('rws').ReconnectingWebSocket;

var EVENTS_NUM = 25;

var events = [];
for(var i = 0; i < EVENTS_NUM; i++){
  events.push({
    gyroscopeX: 0,
    gyroscopeY: 0,
    gyroscopeZ: 0,
    accelerationX: 0,
    accelerationY: 0,
    accelerationZ: 0
  });
}

module.exports = Reflux.createStore({
  listenables: [actions],

  start: function(){
    var self = this;
    var server = new ReconnectingWebSocket('ws://localhost:8282');
    server.onmessage = function (event) {
      if (typeof event.data === 'string') {
          var data = JSON.parse(event.data);

          events.push(data.content);
          if (events.length > 25){
            events.shift();
          }

          self.trigger({
            events: events
          });
      }
      else if (event.data instanceof Blob) {
          console.log('binary');
      }

    };
  }
});
