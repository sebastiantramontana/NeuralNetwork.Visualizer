var registerToolTipDomAccess = registerToolTipDomAccess || ((globalInstanceName) => {

    const thisToolTip = this;

    changeDisplay = (value) => {

        let tooltip = document.getElementById('tooltip-' + globalInstanceName);
        tooltip.style.display = value;
    };

    changeText = (idPart, text) => {
        let elem = document.getElementById('tooltip-' + idPart + '-' + globalInstanceName);
        elem.innerHtml = text;
    };

    window[globalInstanceName].ToolTip = {
        show: (title, text) => {
            thisToolTip.changeText('title', title);
            thisToolTip.changeText('text', text);
            thisToolTip.changeDisplay('block');
        },

        close: () => {
            thisToolTip.changeDisplay('none');
        }
    };
});