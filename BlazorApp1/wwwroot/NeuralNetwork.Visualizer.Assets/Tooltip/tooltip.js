window.createToolTip = (globalInstanceName) => {
    const thisToolTip = this;

    changeDisplay = (value) => {
        let tooltip = document.getElementById("tooltip-" + globalInstanceName);
        tooltip.style.display = value;
    };

    changeText = (idPart, text) => {
        let elem = document.getElementById("tooltip-" + idPart + "-" + globalInstanceName);
        elem.innerHtml = text;
    };

    var objName = "ToolTip";

    window[globalInstanceName][objName] = {
        show: (title, text) => {
            thisToolTip.changeText("title", title);
            thisToolTip.changeText("text", text);
            thisToolTip.changeDisplay("block");
        },

        close: () => {
            thisToolTip.changeDisplay("none");
        }
    };

    return objName;
};


