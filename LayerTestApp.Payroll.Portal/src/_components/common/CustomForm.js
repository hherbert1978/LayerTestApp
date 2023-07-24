import React, { useEffect, useState } from "react";

export default function CustomForm(initialFormValues, setCurrentId, validate) 
{
    const [ values, setValues ] = useState(initialFormValues)
    const [ errors, setErrors ] = useState({})
        
    const handleTextboxChange = e => {
        const { name, value } = e.target
        const fieldValue = { [name]: value }
        setValues({
            ...values,
            ...fieldValue
        })
    }

    const handleCheckboxChange = e => {
        const { name, checked } = e.target
        const fieldValue = { [name]: checked}
        setValues({
            ...values,
            ...fieldValue
        })
        validate(fieldValue)
    }

    const resetForm = () => {
        setValues({
            ...initialFormValues 
        })
        setErrors({})
        setCurrentId(0)
    }

    return {
        values,
        setValues,
        errors,
        setErrors,
        handleTextboxChange,
        handleCheckboxChange,
        resetForm
    };
}