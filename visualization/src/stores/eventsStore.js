'use strict';

var Reflux = require('reflux');
var actions = require('../actions/eventsActions.js');

var ReconnectingWebSocket = require('rws').ReconnectingWebSocket;

var EVENTS_NUM = 25;

var events = [];
for(var i = 0; i < EVENTS_NUM; i++){
  events.push({
    rawGyroscope: {x: 0, y: 0, z: 0 },
    rawAccelerometer: {x: 0, y: 0, z: 0 },
    acceleration: {x: 0, y: 0, z: 0 },
    velocity: {x: 0, y: 0, z: 0 }
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

          if (data.type === 'MotionEvent'){

            events.push(data.content);
            if (events.length > EVENTS_NUM){
              events.shift();
            }

            self.trigger({
              events: events
            });
          } else {
            console.warn('Unsupported type:', data.type);
          }
      }
      else if (event.data instanceof Blob) {
          console.log('binary');
      }

    };
  }
});
