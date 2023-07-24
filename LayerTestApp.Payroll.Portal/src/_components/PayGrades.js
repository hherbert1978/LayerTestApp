import React, { useRef, useState, useEffect } from "react";
import { connect } from "react-redux";
import * as actions from "../_actions/payGrade"
import { Grid, Paper, TableContainer, Table, TableHead, TableBody, TableRow, TableCell, Checkbox, createTheme, ButtonGroup, Button } from "@mui/material";
import { withStyles, ThemeProvider } from "@mui/styles"
import EditIcon from "@mui/icons-material/Edit"
import DeleteIcon from "@mui/icons-material/Delete"
import ConfirmDialog from "./common/ConfirmDialog";
import PayGradeForm from "./PayGradeForm";
import AlertErrorDialog from "./common/AlertErrorDialog";
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";

const createdTheme = createTheme();

const styles = (theme) => ({
        root: {
            "& .MuiTableCell-head": {
                fontSize: "1.0rem"
            }
        },
        paper: {
            margin: createdTheme.spacing(2),
            padding: createdTheme.spacing(2)
        },
        gridContainer: {
            "& .MuiGrid-item": {
                padding: createdTheme.spacing(1),
            }
        },
        tableRow: {
            "&:hover": {
                backgroundColor: createdTheme.palette.info.light + "!important"
            }
        },
        snackbar: {
            '& .MuiAlert-icon': {
                color: "#ffffff !important"
            }
        }
    })

var snackBarText = "";

const PayGrades = ({ classes, ...props }) => {

    const [currentId, setCurrentId] = useState(0)
    
    const [ openConfirmDialog, setOpenConfirmDialog ] = useState(false);
    const [ confirmDialogTitle, setConfirmDialogTitle ]  = useState("");
    const [ confirmDialogMessage, setConfirmDialogMessage ] = useState("");
    
    const [ errors, setErrors ] = useState({})

    const [ openAlertErrorDialog, setOpenAlertErrorDialog ] = useState(false);
    const [ alertErrorDialogTitle, setAlertErrorDialogTitle ]  = useState("");

    const [ openSuccessSnackBar, setOpenSuccessSnackBar ] = useState(false);

    const onConfirm = useRef(null);

    const handleConfirm = () => {
        onConfirm.current()
    }

    useEffect(() => {
        props.getAllPayGrades()
    }, [])

    const handleDelete = payGradeId => {

        setOpenConfirmDialog(true)
        setConfirmDialogTitle("Delete PayGrade Confirmation")
        setConfirmDialogMessage("Do you really want to delete PayGrade with PayGradeId='" + payGradeId.toString() + "'")

        const onSuccess = (responseText) =>  
        {
            console.log(responseText)
            snackBarText = responseText
            setOpenSuccessSnackBar(true);
        }

        const onFailure = (errors, errorDialogTitle) => {
            console.log(errors)
            setErrors(errors)
            setAlertErrorDialogTitle(errorDialogTitle)
            setOpenAlertErrorDialog(true);
        }

        onConfirm.current = () => {
            setOpenConfirmDialog(false)       
            props.deletePayGrade(payGradeId, onSuccess, onFailure)
        }
    }

    return (
        <ThemeProvider theme={createdTheme}>
            <Paper className={classes.paper} elevation={24}>            
                <Grid container className={classes.gridContainer}>
                    <Grid item xs={ 6 }>
                        <TableContainer>
                            <Table>
                                <TableHead className={classes.root}>
                                    <TableRow>
                                        <TableCell>
                                            PayGradeId
                                        </TableCell>
                                        <TableCell>
                                            PayGradeName
                                        </TableCell>
                                        <TableCell>
                                            Aktiv
                                        </TableCell>
                                        <TableCell />
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        props.payGradeList.map((record, index) => {                                            
                                            return (
                                                <TableRow key={index} hover className={classes.tableRow}>
                                                    <TableCell>
                                                        {record.payGradeId}
                                                    </TableCell>
                                                    <TableCell>
                                                        {record.payGradeName}
                                                    </TableCell>
                                                    <TableCell>
                                                        <Checkbox checked={record.isActive} />                                         
                                                    </TableCell>
                                                    <TableCell>
                                                        <ButtonGroup variant="text">
                                                            <Button>
                                                                <EditIcon 
                                                                    color="primary"
                                                                    onClick={() => { setCurrentId(record.payGradeId) }} />
                                                            </Button>
                                                            <Button>
                                                                <DeleteIcon 
                                                                    color="secondary"
                                                                    onClick={() => handleDelete(record.payGradeId)} />
                                                            </Button>
                                                        </ButtonGroup>
                                                    </TableCell>
                                                </TableRow>
                                            )
                                        })
                                    }
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Grid>
                    <Grid item xs={ 6 }>
                        <PayGradeForm {...({ currentId, setCurrentId  })} />
                    </Grid>
                </Grid>
                <AlertErrorDialog {...({ openAlertErrorDialog,
                                     setOpenAlertErrorDialog,
                                     alertErrorDialogTitle,
                                     errors })} /> 
                <ConfirmDialog {...({ openConfirmDialog,
                                  setOpenConfirmDialog,
                                  confirmDialogTitle,
                                  confirmDialogMessage,
                                  handleConfirm })} />
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
            </Paper>
        </ThemeProvider>             
    );
}

const mapStateToProps = state => {
    return {
        payGradeList: state.payGrade.list
    }
}

const mapActionToProps = {
    getAllPayGrades: actions.getAllPayGrades,
    deletePayGrade: actions.deletePayGrade
}

export default connect(mapStateToProps, mapActionToProps) (withStyles(styles) (PayGrades));