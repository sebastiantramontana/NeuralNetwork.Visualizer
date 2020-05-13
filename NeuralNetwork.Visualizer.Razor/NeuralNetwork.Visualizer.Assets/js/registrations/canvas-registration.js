var registerCanvasDomAccess = registerCanvasDomAccess || ((globalInstanceName) => {

    const canvasId = 'neuralnetwork-visualizer-canvas-' + globalInstanceName;

    let offscreenCanvas = nul;
    let currentContext = null;

    getCanvasElement = () => {
        const canvas = document.getElementById(canvasId);
        return canvas;
    };

    beginDrawing = () => {
        canvas = getCanvasElement();

        offScreenCanvas = new OffscreenCanvas(canvas.offsetWidth, canvas.offsetHeight);
        currentContext = offScreenCanvas.getContext("2d");
    };

    endDrawing = () => {
        const canvas = getCanvasElement();
        const bitmap = offscreenCanvas.transferToImageBitmap();

        canvas.transferFromImageBitmap(bitmap);
    }

    window[globalInstanceName].Canvas = {

       
    };
});