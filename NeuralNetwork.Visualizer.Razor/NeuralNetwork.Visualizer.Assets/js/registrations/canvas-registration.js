var registerCanvasDomAccess = registerCanvasDomAccess || ((globalInstanceName) => {

    const MINIMUM_FONT_SIZE = 8;

    let _currentContext = null;

    const getCanvasElement = () => {
        const canvasId = 'neuralnetwork-visualizer-canvas-' + globalInstanceName;

        const canvas = document.getElementById(canvasId);
        return canvas;
    };

    const getCurrentContext = () => {
        return _currentContext;
    };

    const getReadyContext = () => {
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

    const verticalAlignment = Object.freeze({
        top: "Top",
        middle: "Middle",
        bottom: "bottom"
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

            switch (pen.cap) {
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

    const drawText = (text, font, x, y, maxWidth, angle) => {

        const getTextBaseline = () => {

            let textBaseline;

            switch (font.verticalAlignment) {
                case null:
                case undefined:
                case verticalAlignment.middle:
                    textBaseline = "middle";
                    break;
                case verticalAlignment.top:
                    textBaseline = "top";
                    break;
                case verticalAlignment.bottom:
                    textBaseline = "bottom";
                    break;
                default:
                    throw "Unknown Vertical Alignment: " + font.verticalAlignment;
            }

            return textBaseline;
        };

        const measureIfTextVisible = (context) => {
            const textSize = context.measureText(text);
            return (textSize.width >= MINIMUM_FONT_SIZE);
        };

        const rotateText = () => {

            if (!angle || angle === 0 || angle === 360)
                return;

            context.translate(x, y);
            context.rotate(angle * (Math.PI / 180));
        }

        const context = getReadyContext();

        context.font = font.css;

        if (!measureIfTextVisible(context))
            return;

        context.fillStyle = getBrushStyle(font.brush);
        context.textAlign = font.textAlign;
        context.textBaseline = getTextBaseline();

        rotateText();

        context.fillText(text, x, y, maxWidth);
    };

    const drawShape = (pen, brush, drawShapeFunc) => {
        const context = getReadyContext();

        context.fillStyle = getBrushStyle(brush);
        configureStroke(pen, context);

        drawShapeFunc(context);

        context.fill();
        context.stroke();
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
                drawShape(pen, brush, (context) => context.ellipse(x, y, radiusX, radiusY, 0, 0, Math.PI * 2));
            },

            drawRectangle: (rectangle, pen, brush) => {
                drawShape(pen, brush, (context) => context.rect(rectangle.x, rectangle.y, rectangle.width, rectangle.height));
            },

            drawLine: (position1, position2, pen) => {
                const context = getReadyContext();

                context.moveTo(position1.x, position1.y);
                context.lineTo(position2.x, position2.y);

                configureStroke(pen, context);
                context.stroke();
            },

            drawText: (text, font, rectangle, angle) => {
                drawText(text, font, rectangle, angle);
            }
        }
    };
});