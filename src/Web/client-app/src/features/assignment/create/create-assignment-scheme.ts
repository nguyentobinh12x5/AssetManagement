import * as Yup from 'yup';

export const CreateAssignmentScheme = Yup.object().shape({
  user: Yup.object({
    fullName: Yup.string().required(''),
    id: Yup.string().required(),
  }).required(),
  asset: Yup.object({
    name: Yup.string().required(''),
    id: Yup.string().required(''),
  }).required(),
  assignedDate: Yup.string().required(''),
  note: Yup.string().required(''),
});

export interface ICreateAssignmentForm
  extends Yup.InferType<typeof CreateAssignmentScheme> {}
