
package com.kendoui.taglib.window;

import com.kendoui.taglib.FunctionTag;

import com.kendoui.taglib.WindowTag;


import javax.servlet.jsp.JspException;

@SuppressWarnings("serial")
public class DragendFunctionTag extends FunctionTag /* interfaces *//* interfaces */ {
    
    @Override
    public int doEndTag() throws JspException {
//>> doEndTag


        WindowTag parent = (WindowTag)findParentWithClass(WindowTag.class);


        parent.setDragend(this);

//<< doEndTag

        return super.doEndTag();
    }

    @Override
    public void initialize() {
//>> initialize
//<< initialize

        super.initialize();
    }

    @Override
    public void destroy() {
//>> destroy
//<< destroy

        super.destroy();
    }

//>> Attributes

    public static String tagName() {
        return "window-dragend";
    }

//<< Attributes

}