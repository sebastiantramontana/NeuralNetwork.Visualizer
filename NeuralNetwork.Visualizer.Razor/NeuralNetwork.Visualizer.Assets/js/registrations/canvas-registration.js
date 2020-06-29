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

    const getBrushStyle = (brush, context) => {

        if (!brush)
            return;

        const getSolidBrush = () => {
            return brush.color.css;
        };

        const getLinearGradienBrush = () => {
            const gradient = context.createLinearGradient(brush.x1, brush.y1, brush.x2, brush.y2);
            gradient.addColorStop(0, brush.color1.css);
            gradient.addColorStop(1, brush.color2.css);

            return gradient;
        };

        let style = null;

        switch (brush.typeDiscriminator) {
            case brushTypes.solid:
                style = getSolidBrush();
                break;
            case brushTypes.linearGradient:
                style = getLinearGradienBrush();
                break;
            default:
                throw "Unknown Brush type: " + brush.typeDiscriminator;
        }

        return style;
    };

    const configureStroke = (pen, context) => {

        if (!pen)
            return;

        context.strokeStyle = getBrushStyle(pen.brush, context);
        context.lineWidth = pen.width;
        context.setLineDash(pen.lineDash);
        context.lineCap = pen.cap;
    };

    const drawText = (text, font, x, y, maxWidth, angle) => {

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

        context.fillStyle = getBrushStyle(font.brush, context);
        context.textAlign = font.textAligment;
        context.textBaseline = font.textBaseline;

        rotateText();

        context.fillText(text, x, y, maxWidth);
    };

    const drawShape = (pen, brush, drawShapeFunc) => {
        const context = getReadyContext();

        context.fillStyle = getBrushStyle(brush, context);
        configureStroke(pen, context);

        drawShapeFunc(context);

        context.fill();
        context.stroke();
    };

    window[globalInstanceName].Canvas = {

        beginDraw: () => {
            const canvas = getCanvasElement();

            const dpi = window.devicePixelRatio;
            canvas.width = canvas.offsetWidth * dpi;
            canvas.height = canvas.offsetHeight * dpi;

            _currentContext = canvas.getContext("2d");

            //TODO: MAKE OFFSCREEN CANVAS!!!
        },
        endDraw: () => {
            _currentContext = null;

            //TODO: MAKE OFFSCREEN CANVAS!!!
        },
        Drawing:
        {
            drawEllipse: (x, y, radiusX, radiusY, pen, brush) => {
                //console.log("x, y, radiusX, radiusY = %s, %s, %s, %s", x, y, radiusX, radiusY);
                drawShape(pen, brush, (context) => context.ellipse(x, y, radiusX, radiusY, 0, 0, Math.PI * 2));
            },

            drawRectangle: (rectangle, pen, brush) => {
                console.log("rectangle, pen, brush = %o %o %o", rectangle, pen, brush);
                drawShape(pen, brush, (context) => context.rect(rectangle.position.x, rectangle.position.y, rectangle.size.width, rectangle.size.height));
            },

            drawLine: (position1, position2, pen) => {
                const context = getReadyContext();

                context.moveTo(position1.x, position1.y);
                context.lineTo(position2.x, position2.y);

                configureStroke(pen, context);
                context.stroke();
            },

            drawText: (text, font, rectangle, angle) => {
                drawText(text, font, rectangle.position.x, rectangle.position.y, rectangle.size.width, angle);
            }
        }
    };
});