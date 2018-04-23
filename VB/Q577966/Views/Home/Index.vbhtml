@CODE
    ViewBag.Title = "GridView - How to allow end-users to resize the grid(MVC)"
END CODE
    <style type="text/css">
        .dxgvStatusBar tr.dxgv > td {
            padding: 0;
        }
        
        .myStatusBar {
            border-top: 0px;
            border-spacing: 0;
            height: 25px;
        }
        
        .draggingContainer {
            height: 25px;
            position: relative;
            width: 100%;
        }
        
        .sizeGrip {
            position: absolute;
            bottom: 0;
            right: 0;
            width: 12px;
            height: 12px;
            background: url("Default.aspx/DXR.axd?r=1_50-Aws08") no-repeat -18px -42px;
            cursor: se-resize;
        }
        
        .ResizeRect {
            position: absolute;
            border: 1px solid #808080;
        }
    </style>
    <script type="text/javascript">
        var startPoint = { x: 0, y: 0 };
        var initialGridSize = { width: 0, height: 0 };

        var postpone = true;

        function GetResizeRect(grid) {
            if(!postpone) return;
            var id = grid.name + "ResizeRect";
            var element = document.getElementById(id);
            if(!element) {
                element = document.createElement("DIV");
                element.id = id;
                element.className = "ResizeRect";
                element.style.display = "none";
                document.body.appendChild(element);
            }
            return element;
        }

        function dragHelper_OnMouseDown(event) {
            var src = ASPxClientUtils.GetEventSource(event);

            startPoint.x = ASPxClientUtils.GetEventX(event);
            startPoint.y = ASPxClientUtils.GetEventY(event);
            initialGridSize.width = grid.GetWidth();
            initialGridSize.height = grid.GetHeight();

            var rectElement = GetResizeRect(grid);
            if(rectElement) {
                var gridMainElement = grid.GetMainElement();

                rectElement.style.left = ASPxClientUtils.GetAbsoluteX(gridMainElement) - 1 + "px";
                rectElement.style.top = ASPxClientUtils.GetAbsoluteY(gridMainElement) - 1 + "px";
                rectElement.style.width = initialGridSize.width + "px";
                rectElement.style.height = initialGridSize.height + "px";

                rectElement.style.display = "";
            }

            var mouseMoveHandler = function (ev) {
                ASPxClientUtils.ClearSelection();
                ResizeGrid(ASPxClientUtils.GetEventX(ev), ASPxClientUtils.GetEventY(ev), GetResizeRect(grid));
            };
            var mouseUpHandler = function (evt) {
                ResizeGrid(ASPxClientUtils.GetEventX(evt), ASPxClientUtils.GetEventY(evt));
                var rectElement = GetResizeRect(grid);
                if(rectElement)
                    rectElement.style.display = "none";

                ASPxClientUtils.DetachEventFromElement(document, "mousemove", mouseMoveHandler);
                ASPxClientUtils.DetachEventFromElement(document, "mouseup", mouseUpHandler);
            };
            ASPxClientUtils.AttachEventToElement(document, "mousemove", mouseMoveHandler);
            ASPxClientUtils.AttachEventToElement(document, "mouseup", mouseUpHandler);
        }

        function ResizeGrid(x, y, rectElement) {
            var deltaX = x - startPoint.x;
            var deltaY = y - startPoint.y;
            var width = initialGridSize.width + deltaX;
            var height = initialGridSize.height + deltaY;

            if(rectElement) {
                rectElement.style.width = width + "px";
                rectElement.style.height = height + "px";
            }
            else {
                grid.SetWidth(width);
                grid.SetHeight(height);
            }
        }
    </script>
<h2>GridView - How to allow end-users to resize the grid (MVC)</h2>
@Html.Partial("GridViewPartial")