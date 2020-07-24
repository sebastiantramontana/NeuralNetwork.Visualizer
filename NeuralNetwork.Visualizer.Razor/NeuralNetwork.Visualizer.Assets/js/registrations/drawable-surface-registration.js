"use strict";

var registerDrawableSurfaceDomAccess = registerDrawableSurfaceDomAccess || ((globalInstanceName) => {

    const getVisualizerElement = () => {
        const visualizerId = 'neuralnetwork-visualizer-' + globalInstanceName;

        const elem = document.getElementById(visualizerId);
        return elem;
    };

    const getCanvasElement = () => {
        const canvasId = 'neuralnetwork-visualizer-canvas-' + globalInstanceName;

        const elem = document.getElementById(canvasId);
        return elem;
    };

    const getElementSize = (element) => {
        return Object.freeze({
            Width: element.offsetWidth,
            Height: element.offsetHeight
        });
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

        },

        clearCanvas: () => {
            const canvas = getCanvasElement();
            const context = canvas.getContext("2d");

            context.clearRect(0, 0, canvas.width, canvas.height);
        }
    };
});