var registerGlobalInstance = registerGlobalInstance || ((globalInstanceName) => {
    window[globalInstanceName] = {};
});