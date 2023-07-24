import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as actions from "../_actions/payGrade"
import {  FormControlLabel, FormGroup, Grid, TextField, Button, createTheme } from "@mui/material";
import Checkbox from '@mui/material/Checkbox';
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";
import { withStyles, ThemeProvider } from "@mui/styles";
import AlertErrorDialog from "./common/AlertErrorDialog";
import CustomForm from "./common/CustomForm";


const initialFormValues  = {
    payGradeId: 0,
    payGradeName: "",
    isActive: true
}

const createdTheme = createTheme();

const styles = (theme) => ({
        root: {
            '& .MuiTextField-root': {
                margin: theme.spacing(1) + "!important",
                minWidth: 250
            }
        },
        formControl: {
            margin: theme.spacing(1) + "!important",
            minWidth: 250
        },
        smMargin: {
            margin: theme.spacing(1) + "!important"
        },
        snackbar: {
            '& .MuiAlert-icon': {
                color: "#ffffff !important"
            }
        }
    });

var snackBarText = "";

const PayGradeForm = ({ classes, ...props }) => {

    console.log(props)
    
    const [ openAlertErrorDialog, setOpenAlertErrorDialog ] = useState(false);
    const [ alertErrorDialogTitle, setAlertErrorDialogTitle ]  = useState("");

    const [ openSuccessSnackBar, setOpenSuccessSnackBar ] = useState(false);

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleTextboxChange,
        handleCheckboxChange,
        resetForm
    } = CustomForm(initialFormValues, props.setCurrentId)

    const handleSubmit = e => {
        e.preventDefault()

        const onSuccess = (responseText) => 
        {
            resetForm();
            snackBarText = responseText
            setOpenSuccessSnackBar(true);
        }

        const onFailure = (errors, errorDialogTitle) => {
            resetForm();
            setErrors(errors)
            setAlertErrorDialogTitle(errorDialogTitle)
            setOpenAlertErrorDialog(true);
        }

        if (props.currentId === 0)
            props.createPayGrade(values, onSuccess, onFailure)
        else
            props.updatePayGrade(values, onSuccess, onFailure)
    }

    useEffect(() => {
        if (props.currentId !== 0) {
            // console.log(props.currentId)
            setValues({
                ...props.payGradeList.find(x => x.payGradeId === props.currentId)
            })
            setErrors({})
            setAlertErrorDialogTitle("")
        }
    }, [props.currentId])

    return (
        <form className={classes.root} onSubmit={handleSubmit} >
            <Grid container>
                <Grid item xs={ 12 }>
                    <TextField 
                        name="payGradeId"
                        variant="outlined"
                        label="Id der Gehaltsklasse"
                        value= { props.currentId }                        
                        fullWidth= { true } 
                        InputProps={{
                            readOnly: true                            
                        }}
                        />
                    <TextField 
                        name="payGradeName"
                        variant="outlined"
                        label="Name der Gehaltsklasse"
                        value= { values.payGradeName }
                        onChange= { handleTextboxChange }
                        fullWidth= { true } />
                    <FormGroup>
                        <FormControlLabel control= {
                            <Checkbox
                                name="isActive"                            
                                checked={values.isActive}
                                onChange={handleCheckboxChange} />
                            }
                            label="Aktiv"
                            labelPlacement="start"
                            sx={{ justifyContent: 'left' }}/>
                    </FormGroup>
                    <div>
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            className={classes.smMargin}
                        >
                            { props.currentId === 0 ? "Anlegen" : "Ändern" }
                        </Button>
                        <Button
                            variant="contained"
                            color="secondary"
                            className={classes.smMargin}
                            onClick={resetForm}
                        >
                            Zurücksetzen
                        </Button>
                    </div>
                </Grid>                    
            </Grid>
            <AlertErrorDialog {...({ openAlertErrorDialog,
                                     setOpenAlertErrorDialog,
                                     alertErrorDialogTitle,
                                     errors })} /> 
            <Snackbar 
                className={classes.snackbar} 
                open={openSuccessSnackBar} 
                onClick={() => {
                    setOpenSuccessSnackBar(false);
                }}
                onClose={() => {
                    setOpenSuccessSnackBar(false);
                }}
                autoHideDuration={2000} 
                anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
                >
                <Alert 
                    severity="success" 
                    sx={{ width: '100%', 
                          background: createdTheme.palette.success.light + "!important", 
                          color: "#ffffff" }}
                >
                    { snackBarText }
                </Alert>
            </Snackbar>

        </form>        
    );
}

const mapStateToProps = state => ({
    payGradeList: state.payGrade.list
})

const mapActionToProps = {
    createPayGrade: actions.createPayGrade,
    updatePayGrade: actions.updatePayGrade
}

export default connect(mapStateToProps,mapActionToProps)(withStyles(styles)(PayGradeForm));
//export default PayGradeForm;