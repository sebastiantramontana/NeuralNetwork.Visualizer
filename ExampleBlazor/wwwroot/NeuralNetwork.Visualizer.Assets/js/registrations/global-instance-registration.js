var registerGlobalInstance = registerGlobalInstance || ((globalInstanceName) => {
    window[globalInstanceName] = {};
    console.log("registerGlobalInstance " + globalInstanceName);
});