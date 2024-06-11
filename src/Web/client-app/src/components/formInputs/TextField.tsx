import React, { InputHTMLAttributes } from 'react';
import { useField } from 'formik';

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string,
    placeholder?: string,
    name: string,
    required?: boolean,
    noValidation?: boolean
};

const TextField: React.FC<InputFieldProps> = (props) => {
    const [field, {error, touched}] = useField(props);
    const { label, required, noValidation} = props;

    const validateClass = () => {
        if (touched && error) return 'is-invalid';
        if (noValidation) return '';
        if (touched) return 'is-valid';
        return '';
    }

    return (
        <div className='form-group row'>
            <label className='col-form-label col-2 d-flex'>
                {label}
                {required && (
                    <div className='invalid ml-1'>*</div>
                )}
            </label>
            <div className='col'>
                <input
                    className={`form-control ${validateClass()}`}
                    {...field}
                    {...props}
                />
                {error && touched && (
                    <div className='invalid'>{error}</div>
                )}
            </div>
        </div>
    );
};

export default TextField;