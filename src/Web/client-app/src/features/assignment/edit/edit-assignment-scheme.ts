import * as Yup from 'yup';

export const EditAssignmentScheme = Yup.object().shape({
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

export interface IEditAssignmentForm
  extends Yup.InferType<typeof EditAssignmentScheme> {
  id: string;
}
