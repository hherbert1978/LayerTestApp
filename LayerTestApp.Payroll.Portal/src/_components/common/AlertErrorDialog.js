import React from 'react';
import { 
    Button,
    createTheme,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
    IconButton
} from '@mui/material';
import { Close } from '@mui/icons-material'

const createdTheme = createTheme();

const AlertErrorDialog = ({...props}) => {
    
    const handleClose = () => {
        props.setOpenAlertErrorDialog(false);
    };

    const errorText = Array.isArray(props.errors) ? 
                        props.errors.map((item, index) => {
                            return <span key={index}>{item}<br /></span>
                        }) : null;

    return (
        <div>
            <Dialog
                open={props.openAlertErrorDialog}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description">
                <IconButton
                    size="small"
                    onClick={handleClose}
                    sx={{ position: "absolute", right: "1rem", top: "1rem" }}
                    > 
                    <Close htmlColor="white" sx={{ backgroundColor: createdTheme.palette.primary.main, 
                                                 borderRadius: 1.5}} />
                </IconButton>
                <DialogTitle id="alert-dialog-title">
                    { props.alertErrorDialogTitle }
                </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        { errorText }
                    </DialogContentText>        
                </DialogContent>
                <DialogActions 
                    sx={{ justifyContent: "start",
                          margin: "1.0rem" }}>
                    <Button 
                        variant="contained"
                        color="primary" 
                        onClick={handleClose} 
                        autoFocus
                        >
                        Ok
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}

export default AlertErrorDialog;