var registerToolTipDomAccess = registerToolTipDomAccess || ((globalInstanceName) => {

    const changeDisplay = (value) => {

        let tooltip = document.getElementById('tooltip-' + globalInstanceName);
        tooltip.style.display = value;
    };

    const changeText = (idPart, text) => {
        let elem = document.getElementById('tooltip-' + idPart + '-' + globalInstanceName);
        elem.innerHtml = text;
    };

    window[globalInstanceName].ToolTip = {
        show: (title, text) => {
            changeText('title', title);
            changeText('text', text);
            changeDisplay('block');
        },

        close: () => {
            changeDisplay('none');
        }
    };
});