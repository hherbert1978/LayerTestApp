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

const ConfirmDialog = ({...props}) => {
    const handleClose = () => {
        props.setOpenConfirmDialog(false);
    };

    return (
        <div>
            <Dialog
                open={props.openConfirmDialog}
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
                    { props.confirmDialogTitle }
                </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        { props.confirmDialogMessage }
                    </DialogContentText>        
                </DialogContent>
                <DialogActions sx={{ justifyContent: "start",
                                     margin: "1.0rem" }}>
                    <Button 
                        variant="contained"
                        color="primary" 
                        onClick={props.handleConfirm} 
                        autoFocus>
                        Ja
                    </Button>
                    <Button 
                        variant="contained"
                        color="secondary" 
                        onClick={handleClose}>
                        Nein
                    </Button>
                    
                </DialogActions>
            </Dialog>
        </div>
    );
}

export default ConfirmDialog;