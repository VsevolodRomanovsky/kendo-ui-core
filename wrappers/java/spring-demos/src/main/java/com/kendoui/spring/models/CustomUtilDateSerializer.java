package com.kendoui.spring.models;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import org.codehaus.jackson.JsonGenerator;
import org.codehaus.jackson.JsonProcessingException;
import org.codehaus.jackson.map.JsonSerializer;
import org.codehaus.jackson.map.SerializerProvider;

public class CustomUtilDateSerializer extends JsonSerializer<Date> {  
    @Override
    public void serialize(Date value, JsonGenerator gen, SerializerProvider sp) throws 
        IOException, JsonProcessingException {      

        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String formattedDate = formatter.format(value.getTime());

        gen.writeString(formattedDate);
    }
}