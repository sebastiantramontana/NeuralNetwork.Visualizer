"use strict";

var registerGlobalInstance = registerGlobalInstance || ((globalInstanceName) => {
    window[globalInstanceName] = {};
});