var registerCanvasDomAccess = registerCanvasDomAccess || ((globalInstanceName) => {

    let _currentContext = null;

    const getCanvasElement = () => {
        const canvasId = 'neuralnetwork-visualizer-canvas-' + globalInstanceName;

        const canvas = document.getElementById(canvasId);
        return canvas;
    };

    const getCurrentContext = () => {
        return _currentContext;
    };

    const createPath = () => {
        const context = getCurrentContext();
        context.beginPath();

        return context;
    };

    const brushTypes = Object.freeze({
        solid: "Solid",
        linearGradient: "LinearGradient"
    });

    const lineStyle = Object.freeze({
        solid: "Solid",
        dash: "Dash",
        dot: "Dot",
        dahsDot: "DahsDot"
    });

    const capStyle = Object.freeze({
        none: "None",
        triangle: "Triangle",
        square: "Square",
        circle: "Circle"
    });

    const getBrushStyle = (brush, context) => {

        if (!brush)
            return;

        const getSolidBrush = () => {
            return brush.color;
        };

        const getLinearGradienBrush = () => {
            const gradient = context.createLinearGradient(brush.x1, brush.y1, brush.x2, brush.y2);
            gradient.addColorStop(0, brush.color1);
            gradient.addColorStop(1, brush.color2);

            return gradient;
        };

        let style = null;

        switch (brush.type) {
            case brushTypes.solid:
                style = getSolidBrush();
                break;
            case brush.linearGradient:
                style = getLinearGradienBrush();
                break;
            default:
                throw "Unknown Brush type: " + brush.type;
        }

        return style;
    };

    const configureStroke = (pen, context) => {

        if (!pen)
            return;

        const getCap = () => {
            let cap = null;

            switch (pen.Cap) {
                case capStyle.none:
                    cap = "butt";
                    break;
                case capStyle.circle:
                    cap = "round";
                    break;
                case capStyle.square:
                case capStyle.triangle:
                    cap = "square";
                    break;
                default:
                    throw "Unknown Cap type:" + pen.cap;
            }

            return cap;
        };

        const getLineDashSegment = () => {
            let segment = null;

            switch (pen.lineStyle) {
                case lineStyle.solid:
                    segment = [];
                    break;
                case lineStyle.dash:
                    segment = [3, 1];
                    break;
                case lineStyle.dot:
                    segment = [pen.width, pen.width];
                    break;
                case lineStyle.dahsDot:
                    segment = [3, 1, pen.width, pen.width];
                    break;
                default:
                    throw "Unknown Line style: " + pen.lineStyle;
            }

            return segment;
        };

        context.strokeStyle = getBrushStyle(pen.brush);
        context.lineWidth = pen.width;
        context.setLineDash(getLineDashSegment());
        context.lineCap = getCap();
    };

    window[globalInstanceName].Canvas = {

        beginDraw: () => {
            canvas = getCanvasElement();

            const offscreenCanvas = new OffscreenCanvas(canvas.offsetWidth, canvas.offsetHeight);
            _currentContext = offscreenCanvas.getContext("2d");
        },
        endDraw: () => {
            const canvas = getCanvasElement();
            const bitmap = _currentContext.canvas.transferToImageBitmap();

            canvas.transferFromImageBitmap(bitmap);
        },
        Drawing:
        {
            drawEllipse: (x, y, radiusX, radiusY, pen, brush) => {
                const context = createPath();

                context.fillStyle = getBrushStyle(brush);
                configureStroke(pen, context);

                context.ellipse(x, y, radiusX, radiusY, 0, 0, Math.PI * 2);

                context.fill();
                context.stroke();
            },

            drawLine: (position1, position2, pen) => {
                const context = createPath();

                context.moveTo(position1.x, position1.y);
                context.lineTo(position2.x, position2.y);

                configureStroke(pen, context);
                context.stroke();
            },

            drawRectangle: (rectangle, pen, brush) => {
                const context = createPath();

                context.fillStyle = getBrushStyle(brush);
                context.fillRect(rectangle.x, rectangle.y, rectangle.width, rectangle.height);

                configureStroke(pen, context);
                context.strokeRect(rectangle.x, rectangle.y, rectangle.width, rectangle.height);
            },

            /*
            
            void DrawText(string text, FontLabel font, Position position);
            void DrawText(string text, FontLabel font, Rectangle rect);
            void DrawText(string text, FontLabel font, Rectangle rect, float angle);
            */
        }
    };
});