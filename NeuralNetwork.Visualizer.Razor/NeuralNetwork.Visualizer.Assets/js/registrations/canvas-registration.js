var registerCanvasDomAccess = registerCanvasDomAccess || ((globalInstanceName) => {

    let _currentContext = null;

    getCanvasElement = () => {
        const canvasId = 'neuralnetwork-visualizer-canvas-' + globalInstanceName;

        const canvas = document.getElementById(canvasId);
        return canvas;
    };

    getCurrentContext = () => {
        return _currentContext;
    };

    const brushTypes = Object.freeze({
        solid: "Solid",
        linearGradient: "LinearGradient"
    });

    applyFillBrush = (rectangle, fillbrush, context) => {
        if (!fillbrush)
            return;

        switch (fillbrush.type) {
            case brushTypes.solid:
                applySolidBrush();
                break;
            case brush.linearGradient:
                applyLinearGradienBrush();
            default:
                throw "Unknown brush type: " + fillbrush.type;
        }

        applySolidBrush = () => {
            context.fillStyle = fillbrush.color;
            context.fill();
        };

        applyLinearGradienBrush = () => {
            const gradient = context.createLinearGradient(rectangle.x, rectangle.y, rectangle.x + rectangle.width, rectangle.y + rectangle.height);
            gradient.addColorStop(0, fillbrush.color1);
            gradient.addColorStop(1, fillbrush.color2);

            ctx.fillStyle = gradient;
            context.fill();
        };
    };


    window[globalInstanceName].Canvas = {

        beginDraw = () => {
            canvas = getCanvasElement();

            const offscreenCanvas = new OffscreenCanvas(canvas.offsetWidth, canvas.offsetHeight);
            _currentContext = offscreenCanvas.getContext("2d");
        },
        endDraw = () => {
            const canvas = getCanvasElement();
            const bitmap = _currentContext.canvas.transferToImageBitmap();

            canvas.transferFromImageBitmap(bitmap);
        },
        Drawing =
        {
            drawEllipse = (rectangle, pen, brush) => {
                const context = getCurrentContext();

                context.beginPath();
                applyFillBrush(brush),

                ctx.fillStyle = 'red';
               
                ctx.ellipse(x, y, radiusX, radiusY, Math.PI * .25, 0, Math.PI * 1.5);
                ctx.fill();
            }

            /*
            void DrawLine(Position p1, Position p2, Pen pen);
            void DrawRectangle(Rectangle rect, Pen pen, IBrush brush);
            void DrawText(string text, FontLabel font, Position position);
            void DrawText(string text, FontLabel font, Rectangle rect);
            void DrawText(string text, FontLabel font, Rectangle rect, float angle);
            */
        }
    };
});