var registerDrawableSurfaceDomAccess = registerDrawableSurfaceDomAccess || ((globalInstanceName) => {

    const visualizerId = 'neuralnetwork-visualizer-' + globalInstanceName;
    const canvasId = 'neuralnetwork-visualizer-canvas-' + globalInstanceName;

    getVisualizerElement = () => {
        const elem = document.getElementById(visualizerId);
        return elem;
    };

    getCanvasElement = () => {
        const elem = document.getElementById(canvasId);
        return elem;
    };

    getElementSize = (element) => {
        return {
            Width: element.offsetWidth,
            Height: element.offsetHeight
        };
    };

    window[globalInstanceName].DrawableSurface = {

        getImage: () => {

            const elem = getCanvasElement();

            return {
                Size: getElementSize(elem),
                Base64Bytes: elem.toDataURL()
            };
        },

        getDrawingSize: () => {
            const elem = getCanvasElement();
            return getElementSize(elem);

        },

        getSize: () => {
            const elem = getVisualizerElement();
            return getElementSize(elem);

        }
    };
});