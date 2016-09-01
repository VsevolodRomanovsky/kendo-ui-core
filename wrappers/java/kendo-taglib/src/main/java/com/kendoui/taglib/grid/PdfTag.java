
package com.kendoui.taglib.grid;


import com.kendoui.taglib.BaseTag;



import com.kendoui.taglib.GridTag;




import com.kendoui.taglib.json.Function;

import javax.servlet.jsp.JspException;

@SuppressWarnings("serial")
public class PdfTag extends  BaseTag  /* interfaces */ /* interfaces */ {
    
    @Override
    public int doEndTag() throws JspException {
//>> doEndTag


        GridTag parent = (GridTag)findParentWithClass(GridTag.class);


        parent.setPdf(this);

//<< doEndTag

        String template;
        if (isSet("template")) {
            template = "kendo.template(jQuery(\"#" + getTemplate() + "\").html())";
            setProperty("template", new Function(template));
        }
        
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
        return "grid-pdf";
    }

    public void setMargin(com.kendoui.taglib.grid.PdfMarginTag value) {
        setProperty("margin", value);
    }

    public boolean getAllPages() {
        return (Boolean)getProperty("allPages");
    }

    public void setAllPages(boolean value) {
        setProperty("allPages", value);
    }

    public java.lang.String getAuthor() {
        return (java.lang.String)getProperty("author");
    }

    public void setAuthor(java.lang.String value) {
        setProperty("author", value);
    }

    public java.lang.Object getAvoidLinks() {
        return (java.lang.Object)getProperty("avoidLinks");
    }

    public void setAvoidLinks(java.lang.Object value) {
        setProperty("avoidLinks", value);
    }

    public java.lang.String getCreator() {
        return (java.lang.String)getProperty("creator");
    }

    public void setCreator(java.lang.String value) {
        setProperty("creator", value);
    }

    public java.util.Date getDate() {
        return (java.util.Date)getProperty("date");
    }

    public void setDate(java.util.Date value) {
        setProperty("date", value);
    }

    public java.lang.String getFileName() {
        return (java.lang.String)getProperty("fileName");
    }

    public void setFileName(java.lang.String value) {
        setProperty("fileName", value);
    }

    public boolean getForceProxy() {
        return (Boolean)getProperty("forceProxy");
    }

    public void setForceProxy(boolean value) {
        setProperty("forceProxy", value);
    }

    public java.lang.String getKeywords() {
        return (java.lang.String)getProperty("keywords");
    }

    public void setKeywords(java.lang.String value) {
        setProperty("keywords", value);
    }

    public boolean getLandscape() {
        return (Boolean)getProperty("landscape");
    }

    public void setLandscape(boolean value) {
        setProperty("landscape", value);
    }

    public java.lang.Object getPaperSize() {
        return (java.lang.Object)getProperty("paperSize");
    }

    public void setPaperSize(java.lang.Object value) {
        setProperty("paperSize", value);
    }

    public java.lang.String getProxyTarget() {
        return (java.lang.String)getProperty("proxyTarget");
    }

    public void setProxyTarget(java.lang.String value) {
        setProperty("proxyTarget", value);
    }

    public java.lang.String getProxyURL() {
        return (java.lang.String)getProperty("proxyURL");
    }

    public void setProxyURL(java.lang.String value) {
        setProperty("proxyURL", value);
    }

    public boolean getRepeatHeaders() {
        return (Boolean)getProperty("repeatHeaders");
    }

    public void setRepeatHeaders(boolean value) {
        setProperty("repeatHeaders", value);
    }

    public float getScale() {
        return (Float)getProperty("scale");
    }

    public void setScale(float value) {
        setProperty("scale", value);
    }

    public java.lang.String getSubject() {
        return (java.lang.String)getProperty("subject");
    }

    public void setSubject(java.lang.String value) {
        setProperty("subject", value);
    }

    public java.lang.String getTemplate() {
        return (java.lang.String)getProperty("template");
    }

    public void setTemplate(java.lang.String value) {
        setProperty("template", value);
    }

    public java.lang.String getTitle() {
        return (java.lang.String)getProperty("title");
    }

    public void setTitle(java.lang.String value) {
        setProperty("title", value);
    }

//<< Attributes

}